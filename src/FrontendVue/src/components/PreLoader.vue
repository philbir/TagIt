<template>
    <div v-if="!lookupStore.isReady">
        <v-container fill-height fluid>
            <v-row align="center" justify="center">
                <v-col class="d-flex justify-center">
                    <div class="text-center">
                        <v-row>
                            <v-col>
                                <v-progress-circular :size="110" color="blue" indeterminate></v-progress-circular>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-subheader class="text-center" style="margin: auto">Starting tag
                                engine...</v-subheader>
                        </v-row>
                        <br />
                    </div>
                </v-col>
            </v-row>
        </v-container>
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
