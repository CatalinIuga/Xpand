<script setup lang="ts">
import PlanetList from "./components/PlanetList.vue";
import Topbar from "./components/Topbar.vue";
</script>

<template>
  <main class="max-w-md flex flex-col justify-center w-full h-screen mx-auto">
    <Topbar />
    <PlanetList />
  </main>
</template>

<style scoped></style>
