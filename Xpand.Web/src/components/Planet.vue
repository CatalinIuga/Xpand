<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import { type PlanetType } from "../types/types.js";

interface Members {
  captain: {
    id: number;
    name: string;
    teamId: number;
  };
  robots: [
    {
      id: number;
      name: string;
      teamId: number;
    }
  ];
}

const props = defineProps<{
  planet: PlanetType;
}>();

const planetImage = ref<string>(
  `http://localhost:5015/static/${props.planet.imageName}`
);

const members = ref<Members | null>();

const getMembers = async () => {
  if (props.planet.teamId) {
    fetch(`http://localhost:5015/api/teams/${props.planet.teamId}/members`)
      .then((res) => res.json())
      .then((team) => {
        members.value = team || null;
      })
      .catch((err) => {
        console.error(err);
        members.value = null;
      });
  }
  // Update the image soruce
  planetImage.value = `http://localhost:5015/static/${props.planet.imageName}`;
};

onMounted(() => {
  getMembers();
});

// Watch for changes in the planet, and update the members and image
watch(props.planet, () => getMembers());

// Preatty format the status of the planet
const statusValue = (status: string) => {
  switch (status) {
    case "NotOK":
      return "!OK";
    case "EnRoute":
      return "En Route";
    default:
      return status;
  }
};
</script>

<template>
  <!-- Planet name and image -->
  <div class="flex flex-col justify-center">
    <img class="w-20" :src="planetImage" alt="vite.svg" />
    <h3 class="text-center text-xl font-bold">{{ planet.name }}</h3>
  </div>
  <!-- Only description -->
  <div v-if="!planet.teamId" class="flex-1">{{ planet.description }}</div>

  <!-- Description with members -->
  <div v-else class="flex flex-1 justify-center items-start gap-1 flex-col">
    <div class="line-clamp-2 text-left">"{{ planet.description }}"</div>

    <!-- Captain signature -->
    <div v-if="members" class="text-xs flex gap-1">
      <span class="font-bold">by captain:</span>
      <p class="underline">{{ members.captain.name }}</p>
    </div>

    <!-- Robots List -->
    <div v-if="members" class="flex gap-1 items-center text-sm py-1">
      <span class="font-bold text-sm">Robots: </span>
      <div
        class="overflow-x-hidden text-ellipsis whitespace-nowrap max-w-[200px]"
      >
        {{ members?.robots.map((r) => r.name).join(", ") }}
      </div>
    </div>
  </div>

  <!-- Status -->
  <div
    class="text-lg px-1 font-bold"
    :class="{
      'text-red-500': planet.status === 'NotOK',
      'text-green-500': planet.status === 'OK',
      'text-yellow-500': planet.status === 'TODO',
      'text-blue-500': planet.status === 'EnRoute',
    }"
  >
    {{ statusValue(planet.status) }}
  </div>
</template>
