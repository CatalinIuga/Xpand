<script setup lang="ts">
import { Ref, ref } from "vue";
import { useAuthStore } from "../store/authstore";

interface Props {
  isModalOpen: Ref<boolean>;
  closeModal: () => void;
}

const props = defineProps<Props>();

const authStore = useAuthStore();

const errorMessage = ref("");
const successMessage = ref("");

const resetMessages = () => {
  errorMessage.value = "";
  successMessage.value = "";
};

const submitLogin = async () => {
  resetMessages();
  try {
    const response = await fetch("http://localhost:5015/api/auth/signin", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name: name.value, password: password.value }),
    });

    const data = await response.json();

    if (response.ok && data.token) {
      // update the store
      authStore.setTokken(data.token);
      authStore.setCaptain(data.captain);
      authStore.setLoggedIn(true);

      resetMessages();
      successMessage.value = "You are logged in";
      setTimeout(() => {
        props.closeModal();
      }, 2000);
    } else {
      errorMessage.value = data.message || "Login failed";
    }
  } catch (err) {
    console.error(err);
    errorMessage.value = "An error occurred during login";
  }
};

const sumbitSignup = () => {
  resetMessages();
  fetch("http://localhost:5015/api/captains", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ name: name.value, password: password.value }),
  })
    .then((r) => r.json())
    .then((res) => {
      resetMessages();
      if (!res.id) {
        errorMessage.value = "Sign up failed, please try again.";
        return;
      }
      successMessage.value = "You are signed up. Please sign in.";
    })
    .catch((error) => {
      console.error("Error:", error);
      error.value = error;
    });
};

const name = ref("");
const password = ref("");
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
      <form @submit.prevent="" class="flex flex-col">
        <h3 class="text-2xl font-bold text-center mb-4">
          Sign in to your account
        </h3>

        <!-- Name input -->
        <label class="block mb-4">
          <span class="text-gray-700">Name:</span>
          <input
            class="border border-1 mt-1 block w-full"
            @input="resetMessages"
            v-model="name"
            type="text"
          />
        </label>

        <!-- Password input -->
        <label class="block mb-4">
          <span class="text-gray-700">Password:</span>
          <input
            class="border border-1 mt-1 block w-full"
            @input="resetMessages"
            v-model="password"
            type="password"
          />
        </label>

        <!-- Error message -->
        <p v-if="errorMessage" class="text-red-500 text-sm">
          {{ errorMessage }}
        </p>

        <!-- Success message -->
        <p v-if="successMessage" class="text-green-500 text-sm">
          {{ successMessage }}
        </p>
        <!-- Sign in/Sign up -->
        <div class="flex justify-between">
          <button
            type="button"
            class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
            @click="sumbitSignup"
          >
            Sign up
          </button>
          <button
            type="button"
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            @click="submitLogin"
          >
            Sign in
          </button>
        </div>
      </form>
    </div>
  </div>
</template>
