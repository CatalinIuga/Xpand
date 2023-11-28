import { createPinia } from "pinia";
import { createApp } from "vue";
import App from "./App.vue";
import "./style.css";

const pina = createPinia();

createApp(App).use(pina).mount("#app");
