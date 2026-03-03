<script setup lang="ts">
import { onMounted, onUnmounted, ref } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()
const isMenuOpen = ref(false)
const isAdminDropdownOpen = ref(false)
const isSidebarCollapsed = ref(false)

const handleLogout = () => {
  auth.logout()
  router.push('/login')
}

const closeMenu = () => {
  isMenuOpen.value = false
  isAdminDropdownOpen.value = false
}

const toggleAdminDropdown = () => {
  isAdminDropdownOpen.value = !isAdminDropdownOpen.value
}

const toggleSidebar = () => {
  isSidebarCollapsed.value = !isSidebarCollapsed.value
  localStorage.setItem('sidebar-collapsed', isSidebarCollapsed.value.toString())
}

// Close dropdown when clicking outside
const handleClickOutside = (event: MouseEvent) => {
  const dropdown = document.getElementById('admin-dropdown')
  const button = document.getElementById('admin-dropdown-button')
  if (dropdown && button && !dropdown.contains(event.target as Node) && !button.contains(event.target as Node)) {
    isAdminDropdownOpen.value = false
  }
}

// Add event listener for click outside
onMounted(() => {
  document.addEventListener('click', handleClickOutside)
  // Load sidebar state from localStorage
  const savedState = localStorage.getItem('sidebar-collapsed')
  if (savedState !== null) {
    isSidebarCollapsed.value = savedState === 'true'
  }
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
})
</script>

<template>
  <aside 
    class="sidebar bg-gray-800 text-white h-screen fixed left-0 top-0 z-50 transition-all duration-300 flex flex-col"
    :class="isSidebarCollapsed ? 'w-16' : 'w-64'"
  >
    <!-- Sidebar Header with Toggle Button -->
    <div class="p-4 flex items-center justify-between border-b border-gray-700">
      <h1 v-if="!isSidebarCollapsed" class="text-xl font-bold">InvestTrack</h1>
      <button @click="toggleSidebar" class="p-1 rounded-md hover:bg-gray-700">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path v-if="isSidebarCollapsed" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 5l7 7-7 7M5 5l7 7-7 7" />
          <path v-else stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 19l-7-7 7-7m8 14l-7-7 7-7" />
        </svg>
      </button>
    </div>

    <!-- Navigation Links -->
    <nav class="flex-1 overflow-y-auto py-4">
      <div class="space-y-2 px-2">
        <!-- Public Routes -->
        <router-link 
          v-if="!auth.isAuthenticated"
          to="/" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
          </svg>
          <span v-if="!isSidebarCollapsed">Home</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Home</div>
        </router-link>

        <!-- Authenticated Routes -->
        <router-link 
          v-if="auth.isAuthenticated"
          to="/landing" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
          </svg>
          <span v-if="!isSidebarCollapsed">Home</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Home</div>
        </router-link>

        <router-link 
          v-if="auth.isAuthenticated"
          to="/stock-dashboard" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 12l3-3 3 3 4-4M8 21l4-4 4 4M3 4h18M4 4h16v12a1 1 0 01-1 1H5a1 1 0 01-1-1V4z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Stock Dashboard</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Stock Dashboard</div>
        </router-link>

        <router-link 
          v-if="auth.isAuthenticated"
          to="/search-area" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Stock Search</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Stock Search</div>
        </router-link>

        <router-link 
          v-if="auth.isAuthenticated"
          to="/crypto-dashboard" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Crypto Dashboard</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Crypto Dashboard</div>
        </router-link>

        <router-link 
          v-if="auth.isAuthenticated"
          to="/crypto" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Crypto Search</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Crypto Search</div>
        </router-link>

        <router-link 
          v-if="auth.isAuthenticated"
          to="/portfolio-summary" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Portfolio</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Portfolio</div>
        </router-link>

        <router-link 
          v-if="auth.isAuthenticated"
          to="/transaction-history" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Transactions</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Transactions</div>
        </router-link>

        <!-- Admin Section -->
        <div v-if="auth.isAuthenticated && auth.user?.isAdmin" class="pt-4 mt-4 border-t border-gray-700">
          <div v-if="!isSidebarCollapsed" class="px-3 text-xs uppercase tracking-wider text-gray-400 mb-2">
            Admin
          </div>
          
          <router-link
            to="/users"
            class="sidebar-link group relative"
            active-class="active"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z" />
            </svg>
            <span v-if="!isSidebarCollapsed">Users</span>
            <div v-if="isSidebarCollapsed" class="tooltip">Users</div>
          </router-link>
          
          <router-link
            to="/audit-logs"
            class="sidebar-link group relative"
            active-class="active"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
            </svg>
            <span v-if="!isSidebarCollapsed">Audit Logs</span>
            <div v-if="isSidebarCollapsed" class="tooltip">Audit Logs</div>
          </router-link>
          
          <router-link
            to="/deployment"
            class="sidebar-link group relative"
            active-class="active"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 9l3 3-3 3m5 0h3M5 20h14a2 2 0 002-2V6a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
            </svg>
            <span v-if="!isSidebarCollapsed">Deployment</span>
            <div v-if="isSidebarCollapsed" class="tooltip">Deployment</div>
          </router-link>
        </div>
      </div>
    </nav>

    <!-- User Section -->
    <div class="p-4 border-t border-gray-700">
      <template v-if="!auth.isAuthenticated">
        <router-link 
          to="/login" 
          class="sidebar-link group relative mb-2"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1" />
          </svg>
          <span v-if="!isSidebarCollapsed">Login</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Login</div>
        </router-link>
        <router-link 
          to="/register" 
          class="sidebar-link group relative"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Register</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Register</div>
        </router-link>
      </template>
      <template v-else>
        <router-link 
          to="/account" 
          class="sidebar-link group relative mb-2"
          active-class="active"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
          </svg>
          <span v-if="!isSidebarCollapsed">Account</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Account</div>
        </router-link>
        <button 
          @click="handleLogout" 
          class="sidebar-link group relative w-full text-left"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
          </svg>
          <span v-if="!isSidebarCollapsed">Logout</span>
          <div v-if="isSidebarCollapsed" class="tooltip">Logout</div>
        </button>
      </template>
    </div>
  </aside>

  <!-- Mobile Menu Button - Only visible on small screens -->
  <div class="md:hidden fixed top-4 right-4 z-50">
    <button 
      @click="isMenuOpen = !isMenuOpen"
      class="p-2 rounded-md bg-gray-800 text-white"
    >
      <svg 
        v-if="!isMenuOpen" 
        xmlns="http://www.w3.org/2000/svg" 
        class="h-6 w-6" 
        fill="none" 
        viewBox="0 0 24 24" 
        stroke="currentColor"
      >
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
      </svg>
      <svg 
        v-else 
        xmlns="http://www.w3.org/2000/svg" 
        class="h-6 w-6" 
        fill="none" 
        viewBox="0 0 24 24" 
        stroke="currentColor"
      >
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
      </svg>
    </button>
  </div>

  <!-- Mobile Menu Overlay -->
  <div 
    v-if="isMenuOpen" 
    class="md:hidden fixed inset-0 bg-black bg-opacity-50 z-40"
    @click="isMenuOpen = false"
  ></div>

  <!-- Mobile Menu -->
  <div 
    v-if="isMenuOpen" 
    class="md:hidden fixed top-0 left-0 w-64 h-full bg-gray-800 z-50 transform transition-transform duration-300 ease-in-out"
  >
    <div class="p-4 border-b border-gray-700 flex justify-between items-center">
      <h1 class="text-xl font-bold text-white">InvestTrack</h1>
      <button @click="isMenuOpen = false" class="text-white">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
        </svg>
      </button>
    </div>
    
    <nav class="p-4">
      <div class="space-y-2">
        <!-- Mobile Navigation Links - Same as desktop but with different styling -->
        <router-link 
          v-if="!auth.isAuthenticated"
          to="/" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Home
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/landing" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Home
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/stock-dashboard" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Stock Dashboard
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/search-area" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Stock Search
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/crypto-dashboard" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Crypto Dashboard
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/crypto" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Crypto Search
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/portfolio-summary" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Portfolio
        </router-link>
        <router-link 
          v-if="auth.isAuthenticated"
          to="/transaction-history" 
          class="mobile-link"
          active-class="active"
          @click="isMenuOpen = false"
        >
          Transactions
        </router-link>

        <!-- Admin Section for Mobile -->
        <div v-if="auth.isAuthenticated && auth.user?.isAdmin" class="pt-4 mt-4 border-t border-gray-700">
          <div class="text-xs uppercase tracking-wider text-gray-400 mb-2">Admin</div>
          <router-link
            to="/users"
            class="mobile-link"
            active-class="active"
            @click="isMenuOpen = false"
          >
            Users
          </router-link>
          <router-link
            to="/audit-logs"
            class="mobile-link"
            active-class="active"
            @click="isMenuOpen = false"
          >
            Audit Logs
          </router-link>
          <router-link
            to="/deployment"
            class="mobile-link"
            active-class="active"
            @click="isMenuOpen = false"
          >
            Deployment
          </router-link>
        </div>

        <!-- User Section for Mobile -->
        <div class="pt-4 mt-4 border-t border-gray-700">
          <template v-if="!auth.isAuthenticated">
            <router-link 
              to="/login" 
              class="mobile-link"
              active-class="active"
              @click="isMenuOpen = false"
            >
              Login
            </router-link>
            <router-link 
              to="/register" 
              class="mobile-link"
              active-class="active"
              @click="isMenuOpen = false"
            >
              Register
            </router-link>
          </template>
          <template v-else>
            <router-link 
              to="/account" 
              class="mobile-link"
              active-class="active"
              @click="isMenuOpen = false"
            >
              Account
            </router-link>
            <button 
              @click="handleLogout" 
              class="mobile-link w-full text-left"
            >
              Logout
            </button>
          </template>
        </div>
      </div>
    </nav>
  </div>
</template>

<style scoped>
.sidebar-link {
  @apply flex items-center gap-3 px-3 py-2 rounded-md text-gray-300 hover:bg-gray-700 hover:text-white transition-colors;
}

.sidebar-link.active {
  @apply bg-indigo-600 text-white;
}

.mobile-link {
  @apply block px-3 py-2 rounded-md text-gray-300 hover:bg-gray-700 hover:text-white transition-colors;
}

.mobile-link.active {
  @apply bg-indigo-600 text-white;
}

.tooltip {
  @apply invisible absolute left-full ml-2 px-2 py-1 bg-gray-900 text-white text-xs rounded whitespace-nowrap;
  top: 50%;
  transform: translateY(-50%);
}

.group:hover .tooltip {
  @apply visible;
}
</style>