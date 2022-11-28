<script setup lang="ts">
import { graphql } from '../gql/gql';
import { type FragmentType, useFragment } from '../gql/fragment-masking';

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

const data = useFragment(ThingFragment, props.thing);
</script>

<template>
  <div>
    <h6>{{ data.title }}</h6>
    <v-img :src="data.thumbnail?.url" width="180" />
  </div>
</template>