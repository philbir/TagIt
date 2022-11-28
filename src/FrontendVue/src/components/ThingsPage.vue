<template>
  <v-sheet v-if="data" class="mt-2">
    <v-row>
      <v-col
        v-for="thing in data.things.nodes"
        :key="thing.id"
        md="2"
        sm="6"
        xs="12"
      >
        <ThingItem :thing="thing" />
      </v-col>
    </v-row>
  </v-sheet>
</template>

<script setup lang="ts">
import { useQuery } from "@urql/vue";
import { graphql } from "../gql";
import ThingItem from "./ThingItem.vue";

const searchQueryDocument = graphql(/* GraphQL */ `
  query thingsSearch {
    things {
      nodes {
        ...ThingItem
      }
    }
  }
`);

const { fetching, data, error } = useQuery({
  query: searchQueryDocument,
});

console.log(data.value);
</script>