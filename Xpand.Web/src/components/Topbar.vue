<script setup lang="ts">
import { onMounted, ref } from "vue";

onMounted(() => {
  // test sesion with stored JWT token from localStorage
  isLoggedIn.value = false;
});

const isLoggedIn = ref(false);
</script>

<template>
  <header
    class="bg-primary max-w-md w-full flex items-center justify-between p-4"
  >
    <h1 class="font-sans font-bold text-text uppercase text-xl">Xpand</h1>
    <div v-if="!isLoggedIn" class="flex gap-3">
      <button
        @click="isLoggedIn = true"
        class="px-4 text-black rounded-md bg-green-500 hover:bg-green-400 hover:transition-all hover:duration-500 py-1"
      >
        Sign In
      </button>
      <button
        @click="isLoggedIn = true"
        class="px-4 text-black rounded-md bg-green-500 hover:bg-green-400 hover:transition-all hover:duration-500 py-1"
      >
        Sign Up
      </button>
    </div>
    <div v-else class="">
      <button
        @click="isLoggedIn = false"
        class="px-4 text-black rounded-md bg-green-500 hover:bg-green-400 hover:transition-all hover:duration-500 py-1"
      >
        Sign Out
      </button>
    </div>
  </header>
</template>
