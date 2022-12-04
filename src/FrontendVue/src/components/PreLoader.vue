<template>
    <div v-if="!lookupStore.isReady">
        <v-progress-circular color="primary" indeterminate :size="72" :width="12"></v-progress-circular>
    </div>
    <slot v-else></slot>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import { useCorrenspondentStore } from "@/stores/correspondentStore";
import { useLookupStore } from "@/stores/lookupStore";
import { useReceiverStore } from "@/stores/receiverStore";

const lookupStore = useLookupStore();
const correnspondentStore = useCorrenspondentStore();
const receiverStore = useReceiverStore();

onMounted(() => {
    lookupStore.load();
    correnspondentStore.loadAll();
    receiverStore.loadAll();
});
</script>
