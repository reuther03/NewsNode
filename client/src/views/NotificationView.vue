<script setup lang="ts">
import { onMounted } from 'vue'
import notificationHub from '@/NotificationHub.ts'

export default {
  mounded() {
    notificationHub.client.on('notification', function (notification) {
      store
    });
    notificationHub.start();

  },
}



</script>

<template>
  <div>
    <h1>Notification</h1>
    <ul>
      <li v-for="(notification, index) in notifications" :key="index">
        {{ notification }}
      </li>
    </ul>
  </div>
</template>

<style scoped>
</style>
