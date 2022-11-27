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
    <h3>{{ data.title }}</h3>
    <img :src="data.thumbnail?.url" />
  </div>
</template>