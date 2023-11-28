<script setup lang="ts">
import { computed, ref } from "vue";
import { useAuthStore } from "../store/authstore";
import AuthModal from "./AuthModal.vue";

const authStore = useAuthStore();

const isLoggedIn = computed(() => authStore.isLoggedIn);

const isAuthModalOpen = ref(false);

const closeModal = () => (isAuthModalOpen.value = false);
</script>

<template>
  <header
    class="bg-primary max-w-md flex p-4 fixed top-0 left-1/2 -translate-x-1/2 traslate h-20 w-full justify-between items-center"
  >
    <h1 class="font-sans font-bold text-text uppercase text-xl">Xpand</h1>
    <div v-if="!isLoggedIn" class="flex gap-3">
      <button
        @click="isAuthModalOpen = true"
        class="px-4 text-black rounded-md bg-green-500 hover:bg-green-400 hover:transition-all hover:duration-500 py-1"
      >
        Sign In
      </button>
    </div>
    <div v-if="isLoggedIn" class="">
      <button
        @click="authStore.signOut"
        class="px-4 text-black rounded-md bg-green-500 hover:bg-green-400 hover:transition-all hover:duration-500 py-1"
      >
        Sign Out
      </button>
    </div>
  </header>
  <AuthModal :isModalOpen="isAuthModalOpen" :closeModal="closeModal" />
</template>
