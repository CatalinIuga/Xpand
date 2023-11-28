<script setup lang="ts">
import { defineProps, ref } from "vue";
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

  // image ref
  const image = document.getElementById("image") as HTMLInputElement;

  const formData = new FormData();
  formData.append("name", planetCopy.value.name);
  formData.append("description", planetCopy.value.description);
  formData.append("status", planetCopy.value.status);

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
  error.value = "";
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
      <input id="image" type="file" class="border border-1 mt-1 block w-full" />
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
        <option class="text-sm" value="NotOK">!OK</option>
        <option class="text-sm" value="TODO">TODO</option>
        <option class="text-sm" value="EnRoute">EnRoute</option>
      </select>
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
