<template>
    <v-text-field v-if="value.dataType === 'NUMBER'" type="number" :label="value.name" v-model="input"
        variant="solo"></v-text-field>
    <v-text-field v-if="value.dataType === 'STRING'" :label="value.name" v-model="input" variant="solo"></v-text-field>
    <v-text-field v-if="value.dataType === 'DATE_TIME'" :label="value.name" v-model="input"
        variant="solo"></v-text-field>
</template>

<script setup lang="ts">

import { defineProps, ref, watch } from 'vue';

const props = defineProps({
    value: {
        type: Object,
        required: true,
    },
});

const input = ref(props.value.value);

const emits = defineEmits(["change"]);

watch(input, (newValue) => {

    emits("change", {
        id: props.value.id,
        value: newValue,
        definitionId: props.value.definitionId,

    });
})

</script>
