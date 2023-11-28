<script setup lang="ts">
import { onMounted } from "vue";
import PlanetList from "./components/PlanetList.vue";
import Topbar from "./components/Topbar.vue";
import { useAuthStore } from "./store/authstore";

const authStore = useAuthStore();
onMounted(() => {
  // test sesion with stored JWT token from localStorage
  const token = localStorage.getItem("token");
  if (!token) {
    authStore.setLoggedIn(false);
    return;
  }
  authStore.setTokken(token);
  authStore.checkAuth();

  console.log(authStore.isLoggedIn);
});
</script>

<template>
  <main class="flex max-w-md flex-col justify-center w-full h-screen mx-auto">
    <Topbar />
    <PlanetList />
  </main>
</template>

<style scoped></style>
