<script setup lang="ts">
import { onMounted, ref } from "vue";
import { type PlanetType } from "../types/types.js";
import Planet from "./Planet.vue";
import PlanetModal from "./PlanetModal.vue";

onMounted(async () => {
  const response = await fetch("http://localhost:5015/api/planets");
  const data = await response.json();
  planets.value = data;
});

const planets = ref<Array<PlanetType>>([]);

const isModalOpen = ref(false);
const selectedPlanet = ref<PlanetType>();

const closeModal = () => {
  isModalOpen.value = false;
};

const openModal = (planet: PlanetType) => {
  selectedPlanet.value = planet;
  isModalOpen.value = true;
};
</script>

<template>
  <section class="flex-1 mt-20 w-full gap-2 overflow-y-auto p-2">
    <button
      v-for="planet in planets"
      @click="openModal(planet)"
      class="flex justify-evenly w-full items-center gap-3 border border-gray-600 hover:bg-gray-300 border-opacity-10 rounded-sm p-4 mb-2"
    >
      <Planet :planet="planet" />
    </button>

    <PlanetModal
      :isModalOpen="isModalOpen"
      :selectedPlanet="selectedPlanet"
      :closeModal="closeModal"
    />
  </section>
</template>
