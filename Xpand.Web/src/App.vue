<script setup lang="ts">
import { onMounted } from "vue";
import PlanetList from "./components/PlanetList.vue";
import Topbar from "./components/Topbar.vue";
import { useAuthStore } from "./store/authstore";

const authStore = useAuthStore();

onMounted(async () => {
  await authStore.checkAuth();
});
</script>

<template>
  <main class="flex max-w-md flex-col justify-center w-full h-screen mx-auto">
    <Topbar />
    <PlanetList />
  </main>
</template>

<style scoped></style>
