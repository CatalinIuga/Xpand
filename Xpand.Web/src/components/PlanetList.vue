<script setup lang="ts">
import { onMounted, ref } from "vue";

type Planet = {
  id: number;
  name: string;
  imageName: string;
  description: string;
  status: string;
  robotNumber: number;
  teamId?: number;
};

onMounted(async () => {
  const response = await fetch("http://localhost:5015/api/planets");
  const data = await response.json();
  planets.value = data;
});

const planets = ref<Array<Planet>>([]);
</script>

<template>
  <section class="p-2">
    <article
      v-for="planet in planets"
      class="flex justify-between items-center gap-3 border border-gray-600 border-opacity-10 rounded-sm p-4"
    >
      <div class="flex flex-col justify-center">
        <img
          class="w-12 h-12"
          :src="`http://localhost:5015/static/${planet.imageName}`"
          alt="vite.svg"
        />
        <h3 class="text-center text-lg font-bold">{{ planet.name }}</h3>
      </div>
      <div class="flex"></div>
      <div
        class="text-lg font-bold"
        :class="planet.status === 'OK' ? 'text-green-400' : ''"
      >
        {{ planet.status }}
      </div>
    </article>
  </section>
</template>
