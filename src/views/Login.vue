<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useRouter, useRoute } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()
const route = useRoute()

const email = ref('')
const password = ref('')
const error = ref('')
const successMessage = ref('')
const showError = ref(false)
const errorMessage = ref('')
let errorTimeout: number | null = null

onMounted(() => {
  // Clear any existing errors
  error.value = ''
  
  if (route.query.reset === 'success') {
    successMessage.value = 'Password has been reset successfully. Please login with your new password.'
  }
  
  // Make sure to clear any existing timeouts
  if (errorTimeout) {
    clearTimeout(errorTimeout)
  }
})

const displayError = (message: string) => {
  // Clear any existing timeout
  if (errorTimeout) {
    clearTimeout(errorTimeout)
  }
  
  // Set error message and show it
  errorMessage.value = message
  showError.value = true
  
  // This error won't auto-clear - user must take action
  console.log('Displaying error:', message)
}

const handleSubmit = async () => {
  try {
    await auth.login(email.value, password.value)
    router.push('/landing')
  } catch (e) {
    console.log('Login error:', e)
    displayError('Invalid credentials')
  }
}

// Only clear error when user takes action
const handleInput = () => {
  showError.value = false
}
</script>

<template>
  <div class="min-h-screen flex justify-center items-start bg-gradient-to-br from-gray-100 to-gray-300 py-12 px-4 sm:px-6 lg:px-8">
    <!-- Enhanced login box with branding, informative text, and sleek styling -->
    <div class="max-w-xl w-full space-y-9 bg-white p-12 rounded-lg shadow-2xl">
      
      <!-- Logo and Welcome Message -->
      <div class="text-center">
        <!-- <img src="/logo.png" alt="Stock Navigator Logo" class="mx-auto h-12 w-12" /> -->
        <h2 class="text-2xl font-semibold text-gray-900 mt-4">Login to Track My Investments</h2>
        <p class="text-sm text-gray-600 mt-2">
          Discover insights, stay informed, and make smarter investment decisions.
        </p>
      </div>

      <!-- Display Success Message -->
      <p v-if="successMessage" class="mt-2 text-center text-sm text-green-600">
        {{ successMessage }}
      </p>

      <!-- Custom Error Alert that Won't Auto-Clear -->
      <div v-if="showError" class="p-3 bg-red-100 border border-red-400 text-red-700 rounded relative" role="alert">
        <span class="block sm:inline">{{ errorMessage }}</span>
      </div>

      <!-- Login Form -->
      <form class="mt-8 space-y-6" @submit.prevent="handleSubmit">
        <div class="rounded-md shadow-sm -space-y-px">
          <div>
            <label for="email" class="sr-only">Email address</label>
            <input
              id="email"
              v-model="email"
              type="email"
              required
              @input="handleInput"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
              placeholder="Email address"
            />
          </div>
          <div>
            <label for="password" class="sr-only">Password</label>
            <input
              id="password"
              v-model="password"
              type="password"
              required
              @input="handleInput"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
              placeholder="Password"
            />
          </div>
        </div>

        <!-- Forgot Password Link -->
        <div class="flex justify-end">
          <div class="text-sm">
            <router-link to="/forgot-password" class="font-medium text-indigo-600 hover:text-indigo-500">
              Forgot your password?
            </router-link>
          </div>
        </div>

        <!-- Sign-In Button -->
        <div>
          <button
            type="submit"
            class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
          >
            Sign in
          </button>
        </div>
      </form>

      <!-- Sign-Up Prompt for New Users -->
      <p class="text-center text-sm text-gray-600">
        New to Investment Tracker Pro?
        <router-link to="/register" class="font-medium text-indigo-600 hover:text-indigo-500">
          Create an account
        </router-link>
      </p>
    </div>
  </div>
</template>

