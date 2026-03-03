<script setup lang="ts">
import Navbar from './components/Navbar.vue'
import { ref, onMounted, onUnmounted, watch } from 'vue'

const sidebarCollapsed = ref(false)

onMounted(() => {
  // Load sidebar state from localStorage
  const savedState = localStorage.getItem('sidebar-collapsed')
  if (savedState !== null) {
    sidebarCollapsed.value = savedState === 'true'
  }
  
  // Add event listener for storage changes (for cross-tab sync)
  window.addEventListener('storage', handleStorageChange)
})

// Clean up event listener
onUnmounted(() => {
  window.removeEventListener('storage', handleStorageChange)
})

// Handle storage changes from other tabs
const handleStorageChange = (event: StorageEvent) => {
  if (event.key === 'sidebar-collapsed') {
    sidebarCollapsed.value = event.newValue === 'true'
  }
}

// Watch for changes to sidebar state in localStorage
watch(
  () => sidebarCollapsed.value,
  (newVal, oldVal) => {
    const observer = new MutationObserver(() => {
      const sidebarElement = document.querySelector('.sidebar')
      if (sidebarElement) {
        sidebarCollapsed.value = sidebarElement.classList.contains('w-16')
      }
    })

    const sidebarElement = document.querySelector('.sidebar')
    if (sidebarElement) {
      observer.observe(sidebarElement, { attributes: true })
    }

    // Clean up observer when sidebarCollapsed changes
    return () => observer.disconnect()
  }
)
</script>

<template>
  <div class="flex min-h-screen bg-gray-100">
    <Navbar />
    <main 
      class="flex-1 transition-all duration-300" 
      :class="{ 
        'ml-64': !sidebarCollapsed, 
        'ml-16': sidebarCollapsed 
      }"
    >
      <div class="p-4 sm:p-6">
        <router-view></router-view>
      </div>
    </main>
  </div>
</template>