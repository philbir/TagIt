import { createApp } from "vue";
import App from "./App.vue";
import urql from '@urql/vue';
import router from './router'

const app = createApp(App)
app.use(urql, {
    url: 'http://localhost:5000/graphql',
});
app.use(router)

// Plugins
import { registerPlugins } from '@/plugins'
registerPlugins(app)

app.mount("#app");
