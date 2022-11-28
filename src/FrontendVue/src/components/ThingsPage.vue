<template>
  <v-sheet class="mt-2" :min-height="800">
    <v-row>
      <v-col
        v-for="thing in data.things.nodes"
        :key="thing.id"
        md="4"
        sm="6"
        xs="12"
      >
        <ThingItem :thing="thing" />
      </v-col>
    </v-row>
  </v-sheet>
</template>

<script setup>
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
</script>