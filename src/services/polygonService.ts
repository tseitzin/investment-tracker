import axios from 'axios'
import { secretsService } from './secretService'
import type { 
  PolygonStockSnapshot, 
  PolygonCompanyInfo, 
  StockData, 
  MarketMovers,
  CompanyDetailsResponse,
  CompanyDetails, 
  NewsResponse,
  NewsArticle,
  RelatedCompany,
  RelatedCompaniesResponse,
  HistoricalDataPoint,
  CompanySearchResponse,
  CompanySearchResult
} from '../types/polygon'
import { getDateRange } from '../utils/dateUtils'

export class PolygonService {
  private readonly baseUrl = 'https://api.polygon.io'
  private apiKey: string | null = null

  private async getApiKey(): Promise<string> {
    if (!this.apiKey) {
      this.apiKey = await secretsService.getPolygonApiKey()
    }
    return this.apiKey
  }

  async getStockSnapshot(symbol: string): Promise<StockData> {
    try {
      const apiKey = await this.getApiKey()
      const [snapshotResponse, companyResponse] = await Promise.all([
        axios.get<PolygonStockSnapshot>(
          `${this.baseUrl}/v2/snapshot/locale/us/markets/stocks/tickers/${symbol}?apiKey=${apiKey}`
        ),
        axios.get<PolygonCompanyInfo>(
          `${this.baseUrl}/v3/reference/tickers/${symbol}?apiKey=${apiKey}`
        )
      ])

      const { ticker } = snapshotResponse.data
      const { results: company } = companyResponse.data

      // Check if market is closed (price and volume are 0)
      const isMarketClosed = ticker.day.c === 0 && ticker.day.v === 0
      const currentPrice = isMarketClosed ? ticker.prevDay.c : ticker.day.c
      const marketStatus = isMarketClosed ? 'Market is currently closed. Using previous close price for current price.' : 'Market is open'
      
      return {
        symbol: ticker.ticker,
        companyName: company.name,
        price: currentPrice,
        change: currentPrice - ticker.prevDay.c,
        marketCap: 0, // Not provided in snapshot
        changePercent: ((currentPrice - ticker.prevDay.c)/currentPrice)*100,
        volume: ticker.day.v,
        timestamp: new Date(ticker.updated / 1000000).toISOString(),
        open: ticker.day.o,
        high: ticker.day.h,
        low: ticker.day.l,
        previousClose: ticker.prevDay.c,
        marketStatus
      }
    } catch (error) {
      console.error('Error fetching stock data:', error)
      throw new Error('Failed to fetch stock data')
    }
  }

  async searchCompanies(query: string): Promise<CompanySearchResult[]> {
    try {
      const apiKey = await this.getApiKey()
      const response = await axios.get<CompanySearchResponse>(
        `${this.baseUrl}/v3/reference/tickers?search=${encodeURIComponent(query)}&active=true&sort=ticker&order=asc&limit=10&apiKey=${apiKey}`
      )
      
      return response.data.results.map(result => ({
        ticker: result.ticker,
        name: result.name,
        market: result.market,
        locale: result.locale
      }))
    } catch (error) {
      console.error('Error searching companies:', error)
      throw new Error('Failed to search companies')
    }
  }

  async getCompanyDetails(symbol: string): Promise<CompanyDetails> {
    try {
      const apiKey = await this.getApiKey()
      const today = new Date().toISOString().split('T')[0]
      
      const response = await axios.get<CompanyDetailsResponse>(
        `${this.baseUrl}/v3/reference/tickers/${symbol}?date=${today}&apiKey=${apiKey}`
      )

      return response.data.results
    } catch (error) {
      console.error('Error fetching company details:', error)
      throw new Error('Failed to fetch company details')
    }
  }

  async getMarketMovers(): Promise<MarketMovers> {
    try {
      const apiKey = await this.getApiKey()
      const [gainersResponse, losersResponse] = await Promise.all([
        axios.get(`${this.baseUrl}/v2/snapshot/locale/us/markets/stocks/gainers?apiKey=${apiKey}`),
        axios.get(`${this.baseUrl}/v2/snapshot/locale/us/markets/stocks/losers?apiKey=${apiKey}`)
      ])

      // Check if market is closed (all stocks have 0 volume)
      const isMarketClosed = gainersResponse.data.tickers.every((ticker: any) => ticker.day.v === 0)
      const marketStatus = isMarketClosed ? 'Market is currently closed. Showing previous trading day data.' : ''

      const processStocks = (tickers: any[]) => {
        return tickers
          .slice(0, 10)
          .map((ticker: any) => ({
            symbol: ticker.ticker,
            price: isMarketClosed ? ticker.prevDay.c : ticker.day.c,
            changePercent: isMarketClosed ? 0 : ticker.todaysChangePerc,
            volume: isMarketClosed ? 0 : ticker.day.v,
            marketStatus
          }))
      }

      const gainers = processStocks(gainersResponse.data.tickers)
      const losers = processStocks(losersResponse.data.tickers)

      return { gainers, losers, marketStatus }
    } catch (error) {
      console.error('Error fetching market movers:', error)
      throw new Error('Failed to fetch market movers')
    }
  }

  async getCompanyNews(symbol: string): Promise<NewsArticle[]> {
    try {
      const apiKey = await this.getApiKey()
      const today = new Date()
      const thirtyDaysAgo = new Date(today.setDate(today.getDate() - 30))
        .toISOString()
        .split('T')[0]
      
      const response = await axios.get<NewsResponse>(
        `${this.baseUrl}/v2/reference/news?ticker=${symbol}&published_utc.gt=${thirtyDaysAgo}&order=desc&limit=10&sort=published_utc&apiKey=${apiKey}`
      )
  
      return response.data.results
    } catch (error) {
      console.error('Error fetching company news:', error)
      throw new Error('Failed to fetch company news')
    }
  }

  async getRelatedCompanies(symbol: string): Promise<RelatedCompany[]> {
    try {
      const apiKey = await this.getApiKey()
      const response = await axios.get<RelatedCompaniesResponse>(
        `${this.baseUrl}/v1/related-companies/${symbol}?apiKey=${apiKey}`
      )

      if(response.data.results) {
  
      // Fetch additional details for each company
        const companiesWithDetails = await Promise.all(
          response.data.results.map(async (company) => {
            try {
              const snapshot = await this.getStockSnapshot(company.ticker)
              return {
                ...company,
                name: snapshot.companyName,
                price: snapshot.price,
                previousClose: snapshot.previousClose
              }
            } catch (error) {
              console.error(`Error fetching details for ${company.ticker}:`, error)
              return {
                ...company,
                name: "",
                price: 0,
                previousClose: 0
              }
            }
          })
        )  
        return companiesWithDetails
      }
      return []
    } catch (error) {
      console.error('Error fetching related companies:', error)
      return []
    }
  }

  async getHistoricalData(symbol: string, timeRange: string): Promise<HistoricalDataPoint[]> {
    try {
      const apiKey = await this.getApiKey()
      const { startDate, endDate } = getDateRange(timeRange)
      
      const response = await axios.get(
        `${this.baseUrl}/v2/aggs/ticker/${symbol}/range/1/day/${startDate}/${endDate}?adjusted=true&sort=asc&apiKey=${apiKey}`
      )
  
      return response.data.results || []
    } catch (error) {
      console.error('Error fetching historical data:', error)
      throw new Error('Failed to fetch historical data')
    }
  }
  
} 

export const polygonService = new PolygonService()