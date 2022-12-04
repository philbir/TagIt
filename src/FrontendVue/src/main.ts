import { graphqlClient } from './graphqlClient';
import { createApp } from "vue";
import { createPinia } from 'pinia'
import App from "./App.vue";
import urql from '@urql/vue';
import router from './router'

const app = createApp(App)
app.use(urql, graphqlClient);

app.use(createPinia())
app.use(router)

import { registerPlugins } from '@/plugins'
registerPlugins(app)

app.mount("#app");
