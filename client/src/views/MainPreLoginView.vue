<script setup lang="ts">

import { useAuthStore } from '@/stores/authStore.ts'
import router from '@/router'
import { onMounted } from 'vue'

const authStore = useAuthStore()

onMounted(() => {
  if (authStore.isLoggedIn) {
    router.push('/home');
  }
});

</script>

<template>
  <div v-if="!authStore.isLoggedIn" class="main">
    <div class="logo">
      <p>LOGO</p>
    </div>
    <div class="account">
      <p class="pbold">Join us</p>
      <div class="buttons">
        <RouterLink to="/login">
          <button>Login</button>
        </RouterLink>
        <RouterLink to="/register">
          <button>Register</button>
        </RouterLink>
      </div>
    </div>
  </div>
</template>

<style scoped>
* {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
}

.main {
  margin: 0;
  padding: 0;
  display: flex;
  flex: 1;
}

.account {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  position: relative;
  height: 96vh;
}

.logo {
  display: flex;
  justify-content: center;
  align-items: center;
  color: white;
  width: 40%;
  height: 96vh;
}

.buttons {
  display: flex;
  flex-direction: column;
  margin: 10px;
  gap: 10px;

}


a button {
  background-color: #00ADB5;
  border: none;
  border-radius: 50px;
  padding: 14px 24px;
  margin: 5px;
  text-align: center;
  font-size: 20px;
  width: 12rem;
  justify-content: center;
}


button:hover {
  background-color: #146c70;
  color: white;
}

</style>
