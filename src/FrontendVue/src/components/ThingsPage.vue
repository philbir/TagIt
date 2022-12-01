<template>
    <v-progress-linear v-if="fetching" indeterminate></v-progress-linear>
    <div v-else-if="data" class="d-flex flex-wrap flex-row justify-start">
        <div class="ma-2" v-for="thing in data.things?.nodes" :key="thing.id">
            <ThingItem :thing="thing" />
        </div>
    </div>
</template>

<script setup lang="ts">
import { useQuery } from "@urql/vue";
import { graphql } from "../gql";
import ThingItem from "./ThingItem.vue";

const searchQueryDocument = graphql(/* GraphQL */ `
    query thingsSearch {
        things {
            nodes {
                id
                ...ThingItem
            }
        }
    }
`);

const { fetching, data, error } = useQuery({
    query: searchQueryDocument,
});
</script>
