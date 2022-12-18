<template>
    <v-menu>
        <template v-slot:activator="{ props }">
            <v-icon icon="mdi-auto-fix" v-bind="props"> </v-icon>
        </template>
        <v-list :items="dates" @click:select="handleSelect"> </v-list>
    </v-menu>
</template>

<script setup lang="ts">
import { DateTime } from "luxon";
import { computed } from "@vue/reactivity";

const dates = computed(() => {
    return props.tokens?.map((x: any) => {
        const dt = DateTime.fromISO(x.value);

        return {
            value: dt,
            title: dt.toLocaleString(DateTime.DATE_MED),
        };
    });
});

const handleSelect = (e: any) => {
    emits("change", e.id);
};

const emits = defineEmits(["change"]);

const props = defineProps({
    tokens: Array,
});
</script>
