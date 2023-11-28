<script setup lang="ts">
import { Ref, computed } from "vue";
import { useAuthStore } from "../store/authstore";
import { PlanetType } from "../types/types";
import PlanetFrom from "./PlanetForm.vue";

interface Props {
  isModalOpen: Ref<boolean>;
  selectedPlanet: Ref<PlanetType>;
  closeModal: () => void;
}

defineProps<Props>();

const authStore = useAuthStore();

const isLoggedIn = computed(() => authStore.isLoggedIn);
</script>
<template>
  <div
    v-if="isModalOpen"
    class="fixed inset-0 bg-gray-500 bg-opacity-75 flex items-center justify-center"
  >
    <div class="bg-white relative shadow-md rounded-md p-8 w-96">
      <button
        @click="closeModal"
        class="absolute bg-red-200 font-extrabold text-2xl uppercase text-red-600 flex items-center justify-center h-6 w-6 pb-1 top-2 right-2 rounded-md"
      >
        &times;
      </button>

      <!-- Edit planet from -->
      <PlanetFrom
        v-if="isLoggedIn"
        :planet="selectedPlanet"
        :closeForm="closeModal"
      />

      <p v-if="!isLoggedIn" class="text-center">Please login to edit planets</p>
    </div>
  </div>
</template>
