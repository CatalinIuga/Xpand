<script setup lang="ts">
import { Ref } from "vue";
import { PlanetType } from "../types/types";
import PlanetFrom from "./PlanetForm.vue";

interface Props {
  isModalOpen: Ref<boolean>;
  selectedPlanet: Ref<PlanetType>;
  closeModal: () => void;
}

defineProps<Props>();
</script>
<template>
  <div
    v-if="isModalOpen"
    class="fixed inset-0 bg-gray-500 bg-opacity-75 flex items-center justify-center"
  >
    <div class="bg-white relative shadow-md rounded-md p-8 w-96">
      <button
        @click="closeModal"
        class="absolute bg-red-200 font-extrabold text-red-600 flex justify-center items-center h-7 w-7 top-2 right-2 rounded-md p-1"
      >
        &times;
      </button>
      <PlanetFrom :planet="selectedPlanet" />
    </div>
  </div>
</template>
