<script setup lang="ts">
import { type PlanetType } from "../../types/types.js";

defineProps<{
  planet: PlanetType;
}>();
</script>

<template>
  <div class="flex flex-col w-fit justify-center">
    <img
      class="w-20"
      :src="`http://localhost:5015/static/${planet.imageName}`"
      alt="vite.svg"
    />
    <h3 class="text-center text-xl font-bold">{{ planet.name }}</h3>
  </div>
  <div class="flex gap-1 flex-col">
    <div class="line-clamp-2">
      "{{ planet.description + planet.description }}"
    </div>
    <div class="text-xs flex gap-1">
      <span class="font-bold">by captain:</span>
      <p class="underline">Luke Skywalker</p>
    </div>
    <div class="flex gap-1 items-center text-sm py-1">
      <span class="font-bold text-sm">Robots: </span>
      <div
        class="overflow-x-hidden text-ellipsis whitespace-nowrap max-w-[33%]"
      >
        {{ ["T88", "R2D2", "T88", "R2D2"].map((r) => r).join(", ") }}
      </div>
    </div>
  </div>
  <div
    class="text-lg px-1 font-bold"
    :class="planet.status === 'OK' ? 'text-green-400' : ''"
  >
    {{ planet.status }}
  </div>
</template>
../../types/types
