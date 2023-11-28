<script setup lang="ts">
import { ref } from "vue";
import { type PlanetType } from "../types/types.js";

interface Props {
  planet: PlanetType;
  closeForm: () => void;
}

const props = defineProps<Props>();

const planetCopy = ref({ ...props.planet });

const success = ref("");
const error = ref("");

const submitForm = async () => {
  // Some basic validation
  if (!planetCopy.value.name) {
    error.value = "Name is required";
    return;
  }

  if (!planetCopy.value.description) {
    error.value = "Description is required";
    return;
  }

  if (!planetCopy.value.robotsCount || planetCopy.value.robotsCount < 0) {
    planetCopy.value.robotsCount = 0;
  }

  // image ref
  const image = document.getElementById("image") as HTMLInputElement;

  const formData = new FormData();
  formData.append("name", planetCopy.value.name);
  formData.append("description", planetCopy.value.description);
  formData.append("status", planetCopy.value.status);
  formData.append("robotsCount", planetCopy.value.robotsCount.toString());

  if (planetCopy.value.teamId) {
    formData.append("teamId", planetCopy.value.teamId.toString());
  }

  if (image.files && image.files.length > 0) {
    formData.append("image", image.files[0]);
  }

  error.value = "";

  fetch(`http://localhost:5015/api/planets/${planetCopy.value.id}`, {
    method: "PUT",
    body: formData,
  })
    .then((res) => res.json())
    .then((planet) => {
      success.value = "Planet updated successfully";
      Object.assign(props.planet, planet);
      props.closeForm();
    })
    .catch((err) => {
      console.error(err);
    });
};

const resetForm = () => {
  planetCopy.value = { ...props.planet };
  const image = document.getElementById("image") as HTMLInputElement;
  image.value = "";
  success.value = "";
  error.value = "";
};
</script>

<template>
  <form @submit.prevent="submitForm">
    <!-- Planet name input -->
    <label class="block mb-4">
      <span class="text-gray-700">Name:</span>
      <input
        v-model="planetCopy.name"
        type="text"
        class="border border-1 mt-1 block w-full"
      />
    </label>

    <!-- Optional image input -->
    <label class="block mb-4">
      <span class="text-gray-700">Image:</span>
      <span class="text-red-600 text-xs opacity-50 text-right">*optional*</span>
      <input id="image" type="file" class="border border-1 mt-1 block w-full" />
    </label>

    <!-- Planet description input -->
    <label class="block mb-4">
      <span class="text-gray-700">Description:</span>
      <textarea
        v-model="planetCopy.description"
        class="border border-1 mt-1 block w-full"
      />
    </label>

    <!-- Planet status input -->
    <div class="flex justify-between">
      <label class="block mb-4">
        <span class="text-gray-700">Status:</span>
        <select
          v-model="planetCopy.status"
          class="mt-1 border border-1 text-sm block w-fit"
        >
          <option class="text-sm" value="OK">OK</option>
          <option class="text-sm" value="NotOK">!OK</option>
          <option class="text-sm" value="TODO">TODO</option>
          <option class="text-sm" value="EnRoute">EnRoute</option>
        </select>
      </label>
      <!-- Optional teamId input -->
      <label class="block mb-4">
        <span class="text-gray-700">Team Id:</span>
        <span class="text-red-600 text-xs opacity-50 text-right"
          >*optional*</span
        >
        <input
          v-model="planetCopy.teamId"
          type="text"
          class="border border-1 mt-1 block w-full"
        />
      </label>
    </div>
    <!-- Robot count input -->
    <label>
      <span class="text-gray-700">Robot count:</span>
      <input
        v-model="planetCopy.robotsCount"
        type="number"
        class="border border-1 mt-1 block w-fit"
      />
    </label>

    <div v-if="success" class="w-full text-center">
      <p class="text-green-500">{{ success }}</p>
    </div>

    <div v-if="error" class="w-full text-center">
      <p class="text-red-500">{{ error }}</p>
    </div>

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
