<script setup lang="ts">
import { graphql } from '../gql/gql';
import { type FragmentType, useFragment } from '../gql/fragment-masking';
import { useRouter } from 'vue-router';

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

const onClick = (id: string) => {
  router.push({ name: "things_detail", params: { id } });
}

const data = useFragment(ThingFragment, props.thing);
</script>

<template>
  <div @click="onClick(data.id)">
    <h6>{{ data.title }}</h6>
    <v-img :src="data.thumbnail?.url" width="180" />
  </div>
</template>