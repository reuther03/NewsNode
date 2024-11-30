<script setup lang="ts">

import { onMounted, reactive } from 'vue'
import axiosService from '@/services/axiosService.ts'
import type { ILoginResult } from '@/Results/ILoginResult.ts'
import { useAuthStore } from '@/stores/authStore.ts'
import router from '@/router'
import type { UserIdentityModel } from '@/types/auth.ts'

const authStore = useAuthStore()

const form = reactive({
  email: '',
  password: ''
})

//naprawic to ze ma przekierowywac do home jak jest zalogowany i tak samo w mainpreloginview
onMounted(() => {
  if (authStore.isLoggedIn) {
    router.push('/home'); // Redirect to home if logged in and trying to visit login
  }
});

const handleSubmit = async () => {
  const user = {
    email: form.email,
    password: form.password
  }
  try {
    const result = await axiosService.post<ILoginResult>('/users-module/User/login', user)

    if (result.data.isSuccess) {
      const identity = (result.data.value as unknown) as UserIdentityModel
      authStore.authenticate(identity)
    } else {
      console.error('Error logging in', result.data.message)
      return new Error(result.data.message)
    }

    await router.push('/home')
  } catch (e) {
    console.error('Error', e)
  }
}

</script>

<template>
  <div class="card">
    <div class="test">
      <p class="pbold">Login</p>
    </div>
    <form @submit.prevent="handleSubmit" class="login_card">
      <input v-model="form.email" type="email" id="email" name="email" autocomplete="email" placeholder="Email" />
      <input v-model="form.password" type="password" id="password" name="password" autocomplete="current-password"
             placeholder="Password" />
      <button type="submit">Login</button>
    </form>
  </div>
</template>

<style scoped>
* {
  background-color: #393E46;
}

.card {
  display: flex;
  flex-direction: column;
  justify-content: start;
  align-items: center;
  padding: 40px;
  border-radius: 15px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  width: 300px;
  height: 350px;
  margin: 10% auto;
}

.login_card {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  width: 100%;
}

.login_card input {
  margin-top: 10px;
  margin-bottom: 10px;
  border-radius: 5px;
  width: 90%;
  padding: 10px;
  border: 1px solid #ccc;
  text-decoration: none;
}

.login_card button {
  flex-direction: column;
  align-items: center;
  background-color: #00ADB5;
  border: none;
  border-radius: 50px;
  padding: 14px 24px;
  text-align: center;
  font-size: 20px;
  width: 100%;
}
</style>
