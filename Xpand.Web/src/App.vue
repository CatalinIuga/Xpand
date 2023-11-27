<script setup lang="ts">
import PlanetList from "./components/PlanetList.vue";
import Topbar from "./components/Topbar.vue";
</script>

<template>
  <main class="max-w-md flex flex-col h-full w-screen mx-auto overflow-hidden">
    <Topbar />
    <PlanetList />
  </main>
</template>

<style scoped></style>
