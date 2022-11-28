<template>
    <h1>Details {{ props.id }}</h1>
    <pre>
        {{ data }}
    </pre>
</template>

<script setup lang="ts">
import { useQuery } from "@urql/vue";
import { graphql } from "../gql";

const props = defineProps({
    id: {
        type: String,
        required: true,
    },
});

const thingDetailsQuery = graphql(/* GraphQL */ `
    query getThingById($id: ID!) {
        thingById(id: $id) {
            id
            title
            type {
                name
            }
            source {
                connectorId
                connectorId
            }
            date
            classId
            state
            thumbnail(loadData: true, pageNumber: 1) {
                url
            }
        }
    }
`);

const { fetching, data, error } = useQuery({
    query: thingDetailsQuery,
    variables: { id: props.id },
});
</script>
