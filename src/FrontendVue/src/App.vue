<script setup lang="ts">
import PreLoader from "./components/PreLoader.vue";
import { useLookupStore } from "./stores/lookupStore";

const store = useLookupStore();
</script>

<template>
    <v-app>
        <PreLoader>
            <v-app-bar density="compact">
                <v-app-bar-nav-icon></v-app-bar-nav-icon>

                <v-toolbar-title>TagIt {{ store.isReady }}</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn icon>
                    <v-icon>mdi-dots-vertical</v-icon>
                </v-btn>
            </v-app-bar>
            <v-main>
                <RouterView />
            </v-main>
        </PreLoader>
    </v-app>
</template>
