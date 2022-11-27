import { createApp } from "vue";
import App from "./App.vue";
import urql from '@urql/vue';

import "./assets/main.css";

const app = createApp(App)
app.use(urql, {
    url: 'http://localhost:5000/graphql',
});

app.mount("#app");
