<script setup lang="ts">
import { defineProps, ref } from "vue";
import { type PlanetType } from "../types/types.js";

interface Props {
  planet: PlanetType;
}

const props = defineProps<Props>();

const planetCopy = ref({ ...props.planet });

const submitForm = () => {
  console.log(planetCopy);

  // TODO: send planetCopy.value to the server

  // TODO API should return the new updated planet
  Object.assign(props.planet, planetCopy.value);
};

const resetForm = () => {
  planetCopy.value = { ...props.planet };
};
</script>

<template>
  <form @submit.prevent="submitForm">
    <label class="block mb-4">
      <span class="text-gray-700">Name:</span>
      <input
        v-model="planetCopy.name"
        type="text"
        class="border border-1 mt-1 block w-full"
      />
    </label>

    <label class="block mb-4">
      <span class="text-gray-700">Image:</span>
      <input type="file" class="border border-1 mt-1 block w-full" />
    </label>

    <label class="block mb-4">
      <span class="text-gray-700">Description:</span>
      <textarea
        v-model="planetCopy.description"
        class="border border-1 mt-1 block w-full"
      ></textarea>
    </label>

    <label class="block mb-4">
      <span class="text-gray-700">Status:</span>
      <select
        v-model="planetCopy.status"
        class="mt-1 border border-1 text-sm block w-fit"
      >
        <option class="text-sm" value="OK">OK</option>
        <option class="text-sm" value="!OK">!OK</option>
        <option class="text-sm" value="TODO">TODO</option>
        <option class="text-sm" value="EnRoute">EnRoute</option>
      </select>
    </label>

    <div class="mt-4 flex justify-between">
      <button
        @click="resetForm"
        type="button"
        class="bg-gray-500 hover:bg-gray-700 text-white font-bold py-2 px-4 rounded"
      >
        Reset Form
      </button>
      <button
        type="submit"
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
      >
        Save Changes
      </button>
    </div>
  </form>
</template>
