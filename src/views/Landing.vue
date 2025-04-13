<template>
  <div class="min-h-screen bg-gradient-to-br from-indigo-50 to-blue-100 py-4">
    <div class="container mx-auto px-4">
      <!-- Welcome Section -->
      <div class="bg-white rounded-xl shadow-xl p-4 mb-6 transform transition-all duration-300 hover:shadow-2xl">
        <div class="text-center">
          <div class="flex justify-center mb-2">
            <div class="w-20 h-20 bg-indigo-100 rounded-full flex items-center justify-center">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 3v4M3 5h4M6 17v4m-2-2h4m5-16l2.286 6.857L21 12l-5.714 2.143L13 21l-2.286-6.857L5 12l5.714-2.143L13 3z" />
              </svg>
            </div>
          </div>
          <h1 class="text-3xl font-bold text-gray-900">
            Welcome back, {{ auth.user?.name }}!
          </h1>
          <p class="text-gray-600 max-w-2xl mx-auto">
            Navigate through your financial journey with our comprehensive tools and features.
            Choose from the options below to get started.
          </p>
          <div class="flex justify-center mt-4">
            <div class="w-16 h-1 rounded-full bg-indigo-500 inline-flex"></div>
          </div>
        </div>        
      </div>

      <!-- Navigation Grid -->
      <h2 class="text-2xl font-bold text-center mb-3 text-gray-800">Your Financial Tools</h2>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 max-w-7xl mx-auto">
        <div 
          v-for="(tile, index) in navigationTiles"
          :key="tile.route"
          class="bg-white p-3 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 transform hover:-translate-y-1 border-t-4"
          :class="{
            'border-indigo-500': index % 4 === 0,
            'border-green-500': index % 4 === 1,
            'border-orange-500': index % 4 === 2,
            'border-purple-500': index % 4 === 3,
            'md:col-span-2 lg:col-span-3 lg:w-1/3 lg:mx-auto': 
              index === navigationTiles.length - 1 && navigationTiles.length % 3 === 1
          }"
        >
          <div class="flex flex-col items-center">
            <div class="w-16 h-16 rounded-full flex items-center justify-center mb-4"
              :class="{
                'bg-indigo-100': index % 4 === 0,
                'bg-green-100': index % 4 === 1,
                'bg-orange-100': index % 4 === 2,
                'bg-purple-100': index % 4 === 3
              }">
              <!-- Stock Dashboard Icon -->
              <svg v-if="tile.title === 'Stock Dashboard'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 12l3-3 3 3 4-4M8 21l4-4 4 4M3 4h18M4 4h16v12a1 1 0 01-1 1H5a1 1 0 01-1-1V4z" />
              </svg>
              
              <!-- Stock Search Icon -->
              <svg v-else-if="tile.title === 'Stock Search'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-green-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
              
              <!-- Crypto Dashboard Icon -->
              <svg v-else-if="tile.title === 'Crypto Dashboard'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-orange-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
              
              <!-- Crypto Search Icon -->
              <svg v-else-if="tile.title === 'Crypto Search'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-purple-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                <circle cx="15" cy="9" r="1" fill="currentColor" />
              </svg>
              
              <!-- Portfolio Summary Icon -->
              <svg v-else-if="tile.title === 'Portfolio Summary'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
              </svg>
              
              <!-- Transaction History Icon -->
              <svg v-else-if="tile.title === 'Transaction History'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-green-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 8v-4a2 2 0 00-2-2h-4a2 2 0 00-2 2v4" />
              </svg>
              
              <!-- Account Settings Icon -->
              <svg v-else-if="tile.title === 'Account Settings'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-orange-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
              </svg>
              
              <!-- User Management Icon -->
              <svg v-else-if="tile.title === 'User Management'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-purple-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z" />
              </svg>
              
              <!-- Audit Logs Icon -->
              <svg v-else-if="tile.title === 'Audit Logs'" xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
              </svg>
              
              <!-- Default Icon (fallback) -->
              <svg v-else xmlns="http://www.w3.org/2000/svg" class="h-8 w-8" fill="none" viewBox="0 0 24 24" stroke="currentColor"
                :class="{
                  'text-indigo-600': index % 4 === 0,
                  'text-green-600': index % 4 === 1,
                  'text-orange-600': index % 4 === 2,
                  'text-purple-600': index % 4 === 3
                }">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z" />
              </svg>
            </div>
            <h3 class="text-xl font-semibold text-gray-800 mb-3">{{ tile.title }}</h3>
            <p class="text-gray-600 text-center">{{ tile.description }}</p>
            <router-link 
              :to="tile.route"
              class="mt-6 px-4 py-2 rounded-lg text-white font-medium transition-colors duration-300"
              :class="{
                'bg-indigo-600 hover:bg-indigo-700': index % 4 === 0,
                'bg-green-600 hover:bg-green-700': index % 4 === 1,
                'bg-orange-600 hover:bg-orange-700': index % 4 === 2,
                'bg-purple-600 hover:bg-purple-700': index % 4 === 3
              }"
            >
              Go to {{ tile.title }}
            </router-link>
          </div>
        </div>
      </div>

      <div class="mt-8 bg-gradient-to-r from-indigo-50 to-blue-50 rounded-lg p-6 shadow-md">
          <h2 class="text-xl font-semibold mb-4 text-gray-800">Quick Overview</h2>
          <div class="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-4 max-w-7xl mx-auto">
            <!-- Saved Stocks -->
            <div 
              @click="router.push('/stock-dashboard')"
              class="bg-white p-4 rounded-lg text-center cursor-pointer hover:bg-indigo-50 transition-colors shadow-sm hover:shadow-md transform hover:-translate-y-1 duration-300"
            >
              <div class="flex justify-center mb-2">
                <div class="w-10 h-10 bg-indigo-100 rounded-full flex items-center justify-center">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 12l3-3 3 3 4-4M8 21l4-4 4 4M3 4h18M4 4h16v12a1 1 0 01-1 1H5a1 1 0 01-1-1V4z" />
                  </svg>
                </div>
              </div>
              <h3 class="text-sm text-gray-500">Saved Stocks</h3>
              <p class="text-2xl font-semibold text-indigo-600">{{ savedStocksCount }}</p>
            </div>
            
            <!-- Saved Crypto -->
            <div 
              @click="router.push('/crypto-dashboard')"
              class="bg-white p-4 rounded-lg text-center cursor-pointer hover:bg-orange-50 transition-colors shadow-sm hover:shadow-md transform hover:-translate-y-1 duration-300"
            >
              <div class="flex justify-center mb-2">
                <div class="w-10 h-10 bg-orange-100 rounded-full flex items-center justify-center">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-orange-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
              </div>
              <h3 class="text-sm text-gray-500">Saved Crypto</h3>
              <p class="text-2xl font-semibold text-orange-600">{{ savedCryptoCount }}</p>
            </div>
            
            <!-- Owned Stocks -->
            <div 
              @click="router.push('/portfolio-summary')"
              class="bg-white p-4 rounded-lg text-center cursor-pointer hover:bg-green-50 transition-colors shadow-sm hover:shadow-md transform hover:-translate-y-1 duration-300"
            >
              <div class="flex justify-center mb-2">
                <div class="w-10 h-10 bg-green-100 rounded-full flex items-center justify-center">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 12l3-3 3 3 4-4M8 21l4-4 4 4M3 4h18M4 4h16v12a1 1 0 01-1 1H5a1 1 0 01-1-1V4z" />
                  </svg>
                </div>
              </div>
              <h3 class="text-sm text-gray-500">Stocks Owned</h3>
              <p class="text-2xl font-semibold text-green-600">{{ ownedStocksCount }}</p>
            </div>
            
            <!-- Owned Crypto -->
            <div 
              @click="router.push('/portfolio-summary')"
              class="bg-white p-4 rounded-lg text-center cursor-pointer hover:bg-purple-50 transition-colors shadow-sm hover:shadow-md transform hover:-translate-y-1 duration-300"
            >
              <div class="flex justify-center mb-2">
                <div class="w-10 h-10 bg-purple-100 rounded-full flex items-center justify-center">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-orange-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
              </div>
              <h3 class="text-sm text-gray-500">Crypto Owned</h3>
              <p class="text-2xl font-semibold text-purple-600">{{ ownedCryptoCount }}</p>
            </div>
            
            <!-- Last Login -->
            <div class="bg-white p-4 rounded-lg text-center shadow-sm">
              <div class="flex justify-center mb-2">
                <div class="w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-blue-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
              </div>
              <h3 class="text-sm text-gray-500">Last Login</h3>
              <p class="text-sm font-medium text-gray-800">
                {{ formatDate(auth.user?.previousLoginDate) }}
              </p>
            </div>
          </div>
        </div>

      <!-- Footer -->
      <div class="mt-16 text-center text-gray-600">
        <p>© 2025 Track My Investments. All rights reserved.</p>
        <p class="mt-2 text-sm">Your financial journey, simplified.</p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'
//import NavigationTile from '../components/NavigationTile.vue'
import { stockService } from '../services/stockService';
import { cryptoService } from '../services/cryptoService';
import api from '../api/axios';

const auth = useAuthStore()
const router = useRouter()
const savedStocksCount = ref(0)
const savedCryptoCount = ref(0)
const ownedStocksCount = ref(0)
const ownedCryptoCount = ref(0)

onMounted(async () => {
    if (!auth.isAuthenticated) {
        router.push('/login')
        return
    }
    
    try {
        const [stocks, cryptos, portfolio, cryptoPortfolio] = await Promise.all([
            stockService.getSavedStocks(),
            cryptoService.getSavedCryptos(),
            api.get('/portfolio'),
            api.get('/cryptoportfolio')  // Add this new API call
        ])
        savedStocksCount.value = stocks.length
        savedCryptoCount.value = cryptos.length

        // Count unique stocks in portfolio
        const uniqueStocks = new Set(portfolio.data.map((item: any) => item.symbol))
        ownedStocksCount.value = uniqueStocks.size
        
        // Count unique cryptos in portfolio
        const uniqueCryptos = new Set(cryptoPortfolio.data.map((item: any) => item.symbol))
        ownedCryptoCount.value = uniqueCryptos.size
    } catch (e) {
        console.error('Error fetching counts:', e)
    }
})

const navigationTiles = ref([
  {
    title: 'Stock Dashboard',
    description: 'View and manage your saved stocks in one place. Monitor real-time prices and performance.',
    icon: '../assets/stock-market.png',
    route: '/stock-dashboard'
  },
  {
    title: 'Stock Search',
    description: 'Search for stocks, view detailed information, and add them to your watchlist.',
    icon: '/search-stock.svg',
    route: '/search-area'
  },
  {
    title: 'Crypto Dashboard',
    description: 'Track your cryptocurrency portfolio with live price updates and market data.',
    icon: '/dashboard-crypto.svg',
    route: '/crypto-dashboard'
  },
  {
    title: 'Crypto Search',
    description: 'Explore cryptocurrencies, analyze market data, and track your favorites.',
    icon: '/search-crypto.svg',
    route: '/crypto'
  },
  {
    title: 'Portfolio Summary',
    description: 'View your complete portfolio with performance metrics and analytics.',
    icon: '/portfolio.svg',
    route: '/portfolio-summary'
  },
  {
    title: 'Transaction History',
    description: 'Review your complete history of stock purchases and sales.',
    icon: '/transactions.svg',
    route: '/transaction-history'
  },
  {
    title: 'Account Settings',
    description: 'Manage your profile, security settings, and preferences.',
    icon: '/account.svg',
    route: '/account'
  }
])

const formatDate = (date: string | undefined) => {
    if (!date) return 'N/A'
    return new Date(date).toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    })
}

// Add admin tiles if user is admin
if (auth.user?.isAdmin) {
  navigationTiles.value.push(
    {
      title: 'User Management',
      description: 'Manage user accounts, permissions, and system access.',
      icon: '/users.svg',
      route: '/users'
    },
    {
      title: 'Audit Logs',
      description: 'View system activity, user actions, and security events.',
      icon: '/audit.svg',
      route: '/audit-logs'
    }
  )
}
</script>