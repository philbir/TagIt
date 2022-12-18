<template>
    <v-menu>
        <template v-slot:activator="{ props }">
            <v-icon icon="mdi-auto-fix" v-bind="props"> </v-icon>
        </template>
        <v-list :items="itemValues" @click:select="handleSelect"> </v-list>
    </v-menu>
</template>

<script setup lang="ts">
import { computed } from "@vue/reactivity";

const handleSelect = (e: any) => {
    emits("change", e.id);
};

const emits = defineEmits(["change"]);

const itemValues = computed(() => {
    return props.items?.map((x: any) => ({
        title: `${x.item.name} (${x.scrore})`,
        value: x.item.id,
    }));
});

const props = defineProps({
    items: Array,
});
</script>
