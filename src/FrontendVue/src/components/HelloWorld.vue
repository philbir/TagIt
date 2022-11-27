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

<template>
  <h1>Things</h1>
  <div v-if="fetching">Loading...</div>
  <div v-else-if="error">Oh no... {{ error }}</div>
  <div v-else>
    <ul v-if="data">
      <div v-for="thing in data.things.nodes" :key="thing.id">
        <ThingItem :thing="thing"></ThingItem>
      </div>
    </ul>
  </div>
</template>

<style scoped>
@media (min-width: 1024px) {
  .greetings h1,
  .greetings h3 {
    text-align: left;
  }
}
</style>
