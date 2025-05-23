export interface PolygonStockSnapshot {
    ticker: {
      ticker: string
      todaysChangePerc: number
      todaysChange: number
      updated: number
      day: {
        o: number
        h: number
        l: number
        c: number
        v: number
        vw: number
      }
      min: {
        av: number
        t: number
        n: number
        o: number
        h: number
        l: number
        c: number
        v: number
        vw: number
      }
      prevDay: {
        o: number
        h: number
        l: number
        c: number
        v: number
        vw: number
      }
    }
    status: string
    request_id: string
  }

  export interface PolygonCompanyInfo {
    request_id: string
    results: {
      ticker: string
      name: string
      market: string
      locale: string
      primary_exchange: string
      type: string
      active: boolean
      currency_name: string
      market_cap: number
    }
    status: string
  }
  
  export interface StockData {
    symbol: string
    companyName: string
    price: number
    change: number
    changePercent: number
    volume: number
    marketCap: number
    timestamp: string
    open?: number
    high?: number
    low?: number
    previousClose: number
    marketStatus: string
  }

  export interface MarketMover {
    symbol: string
    price: number
    changePercent: number
    volume: number
    marketStatus?: string
  }
  
  export interface MarketMovers {
    gainers: MarketMover[]
    losers: MarketMover[]
    marketStatus?: string
    lastUpdate?: string
  }

  export interface CompanyAddress {
    address1: string
    city: string
    state: string
    postal_code: string
  }
  
  export interface CompanyBranding {
    logo_url: string
    icon_url: string
  }
  
  export interface CompanyDetails {
    ticker: string
    name: string
    market: string
    locale: string
    primary_exchange: string
    type: string
    active: boolean
    currency_name: string
    cik: string
    composite_figi: string
    share_class_figi: string
    market_cap: number
    phone_number: string
    address: CompanyAddress
    description: string
    sic_code: string
    sic_description: string
    ticker_root: string
    homepage_url: string
    total_employees: number
    list_date: string
    branding: CompanyBranding
    share_class_shares_outstanding: number
    weighted_shares_outstanding: number
    round_lot: number
  }
  
  export interface CompanyDetailsResponse {
    request_id: string
    results: CompanyDetails
    status: string
  }

  export interface NewsPublisher {
    name: string
    homepage_url: string
    logo_url: string
    favicon_url: string
  }
  
  export interface NewsInsight {
    ticker: string
    sentiment: 'positive' | 'negative' | 'neutral'
    sentiment_reasoning: string
  }
  
  export interface NewsArticle {
    id: string
    publisher: NewsPublisher
    title: string
    author: string
    published_utc: string
    article_url: string
    tickers: string[]
    image_url: string
    description: string
    keywords: string[]
    insights: NewsInsight[]
  }
  
  export interface NewsResponse {
    results: NewsArticle[]
    status?: string
    next_url?: string
  }

  export interface RelatedCompany {
    ticker: string
    name?: string
    price?: number
    previousClose?: number

  }
  
  export interface RelatedCompaniesResponse {
    request_id: string
    results: RelatedCompany[]
    status: string
    ticker: string
  }

  export interface HistoricalDataPoint {
    v: number // volume
    vw: number // volume weighted average price
    o: number // open
    c: number // close 
    h: number // high
    l: number // low
    t: number // timestamp
    n: number // number of transactions
  }
  
  export interface HistoricalDataResponse {
    ticker: string
    queryCount: number
    resultsCount: number
    adjusted: boolean
    results: HistoricalDataPoint[]
    status: string
    request_id: string
    count: number
  }

  export interface CompanySearchResult {
    ticker: string
    name: string
    market: string
    locale: string
  }
 
  export interface CompanySearchResponse {
    results: Array<{
      ticker: string
      name: string
      market: string
      locale: string
      active: boolean
    }>
    status: string
    request_id: string
    count: number
  }
  
  export type TimeRange = '1W' | '1M' | '3M' | '6M' | '1Y'
  