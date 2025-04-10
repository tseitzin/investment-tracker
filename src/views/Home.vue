<template>
  <div class="min-h-screen bg-gradient-to-b from-indigo-50 to-white">
    <!-- Hero Section -->
    <div class="container mx-auto px-4 py-6">
      <div class="text-center mb-8">
        <h1 class="text-3xl md:text-4xl font-bold text-indigo-900 mb-3">
          Welcome to Track My Investments
        </h1>
        <p class="text-xl text-gray-700 max-w-3xl mx-auto mb-4 leading-relaxed">
          Your comprehensive platform for tracking stocks and cryptocurrencies, managing portfolios, 
          and making informed investment decisions.
        </p>
        <div class="flex justify-center">
          <div class="w-24 h-1 rounded-full bg-indigo-500 inline-flex"></div>
        </div>
      </div>

      <!-- Key Features Section -->
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 mb-6 max-w-7xl mx-auto">
        <div class="bg-white p-8 rounded-xl shadow-lg hover:shadow-xl transition-shadow border-t-4 border-indigo-500">
          <div class="text-indigo-600 mb-1">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6" />
            </svg>
          </div>
          <h3 class="text-xl font-bold text-gray-800 mb-2">Real-Time Market Data</h3>
          <p class="text-gray-600">
            Access live stock prices, market trends, and comprehensive financial data to stay informed 
            about your investments.
          </p>
        </div>
        <div class="bg-white p-8 rounded-xl shadow-lg hover:shadow-xl transition-shadow border-t-4 border-green-500">
          <div class="text-green-600 mb-1">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z" />
            </svg>
          </div>
          <h3 class="text-xl font-bold text-gray-800 mb-2">Portfolio Management</h3>
          <p class="text-gray-600">
            Track your investments, monitor performance, and manage your portfolio with advanced tools 
            and analytics.
          </p>
        </div>
        <div class="bg-white p-8 rounded-xl shadow-lg hover:shadow-xl transition-shadow border-t-4 border-orange-500">
          <div class="text-orange-600 mb-1">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
          </div>
          <h3 class="text-xl font-bold text-gray-800 mb-2">Cryptocurrency Tracking</h3>
          <p class="text-gray-600">
            Stay updated with real-time cryptocurrency prices, market trends, and portfolio tracking 
            capabilities.
          </p>
        </div>
      </div>

      <!-- How It Works Section -->
      <div class="bg-white rounded-xl shadow-lg p-8 mb-8 max-w-7xl mx-auto">
        <h2 class="text-3xl font-bold text-center mb-4 text-gray-800">How It Works</h2>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          <div class="text-center">
            <div class="bg-indigo-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-md">
              <span class="text-2xl font-bold text-indigo-600">1</span>
            </div>
            <h3 class="font-semibold text-lg mb-2 text-gray-800">Create Account</h3>
            <p class="text-gray-600">Sign up for free and set up your personalized dashboard</p>
          </div>
          <div class="text-center">
            <div class="bg-indigo-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-md">
              <span class="text-2xl font-bold text-indigo-600">2</span>
            </div>
            <h3 class="font-semibold text-lg mb-2 text-gray-800">Build Watchlist</h3>
            <p class="text-gray-600">Add stocks and cryptocurrencies to your watchlist</p>
          </div>
          <div class="text-center">
            <div class="bg-indigo-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-md">
              <span class="text-2xl font-bold text-indigo-600">3</span>
            </div>
            <h3 class="font-semibold text-lg mb-2 text-gray-800">Track Portfolio</h3>
            <p class="text-gray-600">Monitor your investments and track performance</p>
          </div>
          <div class="text-center">
            <div class="bg-indigo-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-md">
              <span class="text-2xl font-bold text-indigo-600">4</span>
            </div>
            <h3 class="font-semibold text-lg mb-2 text-gray-800">Make Decisions</h3>
            <p class="text-gray-600">Use insights and data to make informed investment choices</p>
          </div>
        </div>
      </div>

      <!-- Benefits Section -->
      <div class="max-w-7xl mx-auto mb-8">
        <h2 class="text-3xl font-bold text-center mb-4 text-gray-800">Benefits of Using Our Platform</h2>
        <div class="grid grid-cols-1 md:grid-cols-2 gap-8">
          <div class="bg-white p-8 rounded-xl shadow-lg hover:shadow-xl transition-shadow">
            <h3 class="text-xl font-bold text-indigo-600 mb-2">For New Investors</h3>
            <ul class="space-y-3">
              <li class="flex items-center">
                <span class="text-green-500 mr-2">✓</span>
                <span class="text-gray-700">Easy-to-use interface for tracking investments</span>
              </li>
              <li class="flex items-center">
                <span class="text-green-500 mr-2">✓</span>
                <span class="text-gray-700">Educational resources and market insights</span>
              </li>
              <li class="flex items-center">
                <span class="text-green-500 mr-2">✓</span>
                <span class="text-gray-700">Basic portfolio management tools</span>
              </li>
            </ul>
          </div>
          <div class="bg-white p-8 rounded-xl shadow-lg hover:shadow-xl transition-shadow">
            <h3 class="text-xl font-bold text-indigo-600 mb-2">For Experienced Traders</h3>
            <ul class="space-y-3">
              <li class="flex items-center">
                <span class="text-green-500 mr-2">✓</span>
                <span class="text-gray-700">Advanced analytics and market data</span>
              </li>
              <li class="flex items-center">
                <span class="text-green-500 mr-2">✓</span>
                <span class="text-gray-700">Comprehensive portfolio tracking</span>
              </li>
              <li class="flex items-center">
                <span class="text-green-500 mr-2">✓</span>
                <span class="text-gray-700">Real-time market updates and alerts</span>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <!-- Get Started Section -->
      <div class="text-center max-w-3xl mx-auto bg-gradient-to-r from-indigo-100 to-blue-100 p-8 rounded-xl shadow-md">
        <h2 class="text-3xl font-bold mb-3 text-gray-800">Ready to Start Your Financial Journey?</h2>
        <p class="text-gray-700 mb-4 text-lg">
          Join thousands of investors who are already using our platform to track their investments 
          and make better financial decisions.
        </p>
        <div class="grid grid-cols-1 md:grid-cols-2 gap-8 max-w-2xl mx-auto">
          <router-link 
            to="/login"
            class="bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-4 px-6 rounded-lg shadow-md hover:shadow-lg transition-all duration-300 flex flex-col items-center"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 mb-3" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1" />
            </svg>
            <span class="text-xl">Login</span>
            <span class="text-sm mt-1">Access your account</span>
          </router-link>
          <router-link 
            to="/register"
            class="bg-green-600 hover:bg-green-700 text-white font-bold py-4 px-6 rounded-lg shadow-md hover:shadow-lg transition-all duration-300 flex flex-col items-center"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 mb-3" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
            </svg>
            <span class="text-xl">Register</span>
            <span class="text-sm mt-1">Create a new account</span>
          </router-link>
        </div>
      </div>

      <!-- Testimonials Section -->
      <div class="max-w-7xl mx-auto mt-8 mb-6">
        <h2 class="text-3xl font-bold text-center mb-6 text-gray-800">What Our Users Say</h2>
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div class="bg-white p-6 rounded-xl shadow-md">
            <div class="flex items-center mb-4">
              <div class="bg-indigo-100 rounded-full p-2 mr-3">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                </svg>
              </div>
              <div>
                <h3 class="font-semibold text-gray-800">Sarah J.</h3>
                <p class="text-gray-500 text-sm">New Investor</p>
              </div>
            </div>
            <p class="text-gray-600 italic">"This platform has made it so easy for me to start investing. The interface is intuitive and I love how I can track all my investments in one place."</p>
          </div>
          <div class="bg-white p-6 rounded-xl shadow-md">
            <div class="flex items-center mb-4">
              <div class="bg-indigo-100 rounded-full p-2 mr-3">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                </svg>
              </div>
              <div>
                <h3 class="font-semibold text-gray-800">Michael T.</h3>
                <p class="text-gray-500 text-sm">Experienced Trader</p>
              </div>
            </div>
            <p class="text-gray-600 italic">"The real-time data and comprehensive analytics have significantly improved my trading strategy. This is now my go-to platform for all my investment tracking."</p>
          </div>
          <div class="bg-white p-6 rounded-xl shadow-md">
            <div class="flex items-center mb-4">
              <div class="bg-indigo-100 rounded-full p-2 mr-3">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-indigo-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                </svg>
              </div>
              <div>
                <h3 class="font-semibold text-gray-800">Alex R.</h3>
                <p class="text-gray-500 text-sm">Crypto Enthusiast</p>
              </div>
            </div>
            <p class="text-gray-600 italic">"The cryptocurrency tracking features are outstanding. I can monitor all my crypto investments alongside traditional stocks, giving me a complete view of my portfolio."</p>
          </div>
        </div>
      </div>

      <!-- Footer -->
      <footer class="text-center text-gray-600 mt-16 pb-8">
        <p>© 2025 Track My Investments. All rights reserved.</p>
        <p class="mt-2 text-sm">Helping investors make informed decisions since 2024.</p>
      </footer>
    </div>
  </div>
</template>

<script setup lang="ts">
// No script needed for this static page
</script>

<style scoped>
input[type="number"] {
  appearance: textfield;
  -webkit-appearance: textfield;
  -moz-appearance: textfield;
}
input[type="number"]::-webkit-outer-spin-button,
input[type="number"]::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
</style>