<script setup lang="ts">
import { onMounted } from 'vue';
import { useLookupStore } from './stores/lookupStore';

const lookupStore = useLookupStore();

onMounted(() => {
    lookupStore.load();
})


</script>

<template>
    <v-app>
        <v-app-bar density="compact">
            <v-app-bar-nav-icon></v-app-bar-nav-icon>

            <v-toolbar-title>TagIt {{ lookupStore.isReady }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon>
                <v-icon>mdi-dots-vertical</v-icon>
            </v-btn>
        </v-app-bar>
        <v-main>
            <div v-if="!lookupStore.isReady">
                Loading....
            </div>
            <RouterView v-else />
        </v-main>
    </v-app>
</template>
