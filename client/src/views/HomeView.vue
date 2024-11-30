<script setup lang="ts">

import { useAuthStore } from '@/stores/authStore.ts'
import router from '@/router'

const authStore = useAuthStore()

if (!authStore.isLoggedIn) {
  router.push('/');
}

</script>

<template>
  <div>
    <h1> logged as {{ authStore.user?.email }}</h1>
  </div>
</template>

<style scoped>

</style>
