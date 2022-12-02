<template>
    <div v-if="!lookupStore.isReady">
        <v-progress-circular
            color="primary"
            indeterminate
            :size="72"
            :width="12"
        ></v-progress-circular>
    </div>
    <slot v-else></slot>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import { useLookupStore } from "../stores/lookupStore";

const lookupStore = useLookupStore();

onMounted(() => {
    lookupStore.load();
});
</script>
