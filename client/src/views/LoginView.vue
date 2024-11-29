<script setup lang="ts">

import { useAuthStore } from '@/stores/authStore.ts'

const authStore = useAuthStore()

</script>

<template>
  <div class="main">
    <div class="logo">
      <h1>LOGO</h1>
    </div>
    <div class="account box2">
      <h1>SignUp</h1>
      <div class="buttons">
        <button>Register</button>
        <button>Register</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
* {
  display: flex;
  justify-content: space-between; /* Space between the elements to push them to opposite sides */
  align-items: center; /* Center items vertically */
  padding: 0 20px; /* Add some padding for space between the edges */
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

button {
  background-color: #00ADB5;
  border: none;
  border-radius: 15px;
}


button:hover {
  background-color: #146c70;
}

.box1, .box2 {
  padding: 10px;
  border: 1px solid black;
}

</style>
