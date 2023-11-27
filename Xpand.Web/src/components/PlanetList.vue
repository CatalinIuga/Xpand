<script setup lang="ts">
import { onMounted, ref } from "vue";
import { type PlanetType } from "../../types/types.js";
import Planet from "./Planet.vue";

onMounted(async () => {
  const response = await fetch("http://localhost:5015/api/planets");
  const data = await response.json();
  planets.value = data;
  planets.value = [
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
    ...planets.value,
  ];
});

const planets = ref<Array<PlanetType>>([]);
</script>

<template>
  <section
    class="flex flex-col gap-2 p-2  w-full justify-center h-full overflow-y-scroll"
  >
    <button
      v-for="planet in planets"
      class="flex justify-evenly items-center gap-3 border border-gray-600 border-opacity-10 rounded-sm p-4"
    >
      <Planet :planet="planet" />
    </button>
  </section>
</template>
