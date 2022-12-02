<template>
    <v-progress-linear v-if="fetching" indeterminate></v-progress-linear>
    <v-sheet v-else>
        <v-row>
            <v-col md="6">
                <v-form>
                    <v-row>
                        <v-col xs="6">
                            <v-text-field label="Title"></v-text-field>
                        </v-col>
                    </v-row>
                </v-form>
                <h1>{{ thing?.title }}</h1>
            </v-col>
            <v-col md="6">
                <embed
                    width="100%"
                    :height="display.height.value - 60 + 'px'"
                    :src="pdfUrl"
                    id="plugin"
                />
            </v-col>
        </v-row>
    </v-sheet>

    <v-sheet>
        <pre>
            {{ lookupStore.thingTypes }}
        </pre>
    </v-sheet>
</template>

<script setup lang="ts">
import { useQuery } from "@urql/vue";
import { computed } from "@vue/reactivity";
import { useDisplay } from "vuetify/lib/framework.mjs";
import { graphql, useFragment } from "../gql";
import { ThingDetailFragmentDoc } from "../gql/graphql";
import { useLookupStore } from "../stores/lookupStore";

const display = useDisplay();
const lookupStore = useLookupStore();

const props = defineProps({
    id: {
        type: String,
        required: true,
    },
});

const thing = computed(() =>
    useFragment(ThingDetailFragmentDoc, data.value?.thingById)
);
const pdfUrl = `https://localhost:5001/api/thing/preview/${props.id}/Archived`;

const thingDetailsQuery = graphql(/* GraphQL */ `
    query getThingById($id: ID!) {
        thingById(id: $id) {
            id
            ...ThingDetail
        }
    }

    fragment ThingDetail on Thing {
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
`);

const { fetching, data, error } = useQuery({
    query: thingDetailsQuery,
    variables: { id: props.id },
});
</script>
