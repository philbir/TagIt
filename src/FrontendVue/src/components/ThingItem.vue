<script setup lang="ts">
import { graphql } from "../gql/gql";
import { type FragmentType, useFragment } from "../gql/fragment-masking";
import { useRouter } from "vue-router";
import { ref } from "vue";

const ThingFragment = graphql(/* GraphQL */ `
    fragment ThingItem on Thing {
        id
        title
        type {
            name
        }
        state
        thumbnail(loadData: true, pageNumber: 1) {
            url
        }
    }
`);
const props = defineProps<{
    thing: FragmentType<typeof ThingFragment>;
}>();

const router = useRouter();
const isActive = ref(false);

const handleOut = (e: MouseEvent) => {
    isActive.value = false;
}

const handleOver = (e: MouseEvent) => {
    isActive.value = true;
}

const onClick = (id: string) => {
    router.push({ name: "things_detail", params: { id } });
};

const data = useFragment(ThingFragment, props.thing);
</script>

<template>
    <div class="thing-item-wrapper" @click="onClick(data.id)" @mouseover="handleOver" @mouseout="handleOut">
        <v-img :src="data.thumbnail?.url" width="180" />
        <div class="thing-item-header">
            <p class="text-elipsis">{{ data.title }}</p>
            <small>{{ data.type?.name }} </small>
        </div>
        <div class="thing-item-footer" :class="{ active: isActive }">
            <v-btn size="small" icon="mdi-eye" class="mx-2" color="primary"></v-btn>
            <v-btn size="small" icon="mdi-download" class="mx-2" color="primary"></v-btn>
            <v-btn size="small" icon="mdi-pencil" class="mx-2" color="primary"></v-btn>
        </div>
    </div>
</template>

<style scoped>
.thing-item-wrapper {
    position: relative;
}

.thing-item-header {
    top: 0px;
    width: 100%;
    height: 60px;
    border-top-right-radius: 6px;
    border-top-left-radius: 6px;
    background-color: #2e2e2e;
    opacity: 0.85;
    position: absolute;
    color: #fff;
    font-weight: bolder;
    padding: 6px;
    font-size: 1em;
}

.thing-item-header small {
    font-size: 0.9em;
    font-weight: lighter;
}

.thing-item-footer {
    /*pointer-events: none;*/
    bottom: 0px;
    width: 100%;
    height: 50px;
    background-color: #b3adad;
    opacity: 0;
    position: absolute;
    color: #fff;
    font-weight: bolder;
    padding: 6px;
    font-size: 1em;
    border-radius: 6px;
}

.thing-item-footer.active {
    opacity: 0.8;
    transition: opacity;
    transition-timing-function: ease-in-out;
    transition-duration: 400ms;
    transition-delay: 0s;
}

.text-elipsis {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}
</style>
