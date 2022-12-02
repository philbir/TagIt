<template>
    <v-text-field v-if="value.dataType === 'NUMBER'" type="number" :label="value.name" variant="solo"></v-text-field>
    <v-text-field v-if="value.dataType === 'STRING'" :label="value.name" variant="solo"></v-text-field>
    <v-text-field v-if="value.dataType === 'DATE_TIME'" :label="value.name" variant="solo"></v-text-field>
</template>

<script setup lang="ts">

import { defineProps } from 'vue';

const props = defineProps({
    value: {
        type: Object,
        required: true,
    },
});


</script>
