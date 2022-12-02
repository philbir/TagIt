<template>
    <v-progress-linear v-if="fetching" indeterminate></v-progress-linear>
    <v-sheet v-else>
        <v-row>
            <v-col md="6">
                <v-form>
                    <v-container>
                        <h4>{{ editModel.title }}</h4>
                        <v-row>
                            <v-col xs="12">
                                <v-text-field variant="solo" label="Title" v-model="editModel.title"></v-text-field>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col xs="6">
                                <v-select label="Type" variant="solo" v-model="editModel.typeId"
                                    :items="lookupStore.thingTypes" item-value="id" item-title="name"></v-select>
                            </v-col>
                            <v-col xs="6">
                                <v-select v-if="(classOptions.length > 0)" label="Class" variant="solo"
                                    v-model="editModel.classId" :items="classOptions" item-value="id"
                                    item-title="name"></v-select>
                            </v-col>
                        </v-row>
                        <v-row v-for="prop in properties">
                            <v-col>
                                <ThingPropertyEdit :value="prop"></ThingPropertyEdit>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <pre>
                                    {{ editModel }}
                                </pre>
                            </v-col>
                        </v-row>
                    </v-container>
                </v-form>
            </v-col>
            <v-col md="6">
                <embed width="100%" :height="display.height.value - 60 + 'px'" :src="pdfUrl" id="plugin" />
            </v-col>
        </v-row>
    </v-sheet>
</template>


<script setup lang="ts">
import { useQuery } from "@urql/vue";
import { computed } from "@vue/reactivity";
import { useDisplay } from "vuetify/lib/framework.mjs";
import { graphql, useFragment } from "../gql";
import { ThingDetailFragment, ThingDetailFragmentDoc } from "../gql/graphql";
import { useLookupStore } from "../stores/lookupStore";
import { watch, reactive, onMounted } from "vue";
import ThingPropertyEdit from "./ThingPropertyEdit.vue";

const editModel = reactive<{
    id?: string | null
    title?: string | null
    typeId?: string | null
    classId?: string | null
}>({});

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

const classOptions = computed(() => {
    const types = lookupStore.thingTypes.filter((x: any) => x.id === editModel.typeId);

    if (types.length > 0) {
        return types[0].validClasses;
    }
    return [];
})

const properties = computed(() => {
    const typeClasses = classOptions.value.filter((x: any) => x.id === editModel.classId);

    console.log(typeClasses);
    if (typeClasses.length > 0) {
        return typeClasses[0].properties;
    }

    return [];
});

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
            id
            name
        }
        class {
            id
            name
        }
        source {
            connectorId
            connectorId
        }
        date
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

watch(thing, (newValue) => {
    if (newValue) {
        setModel(newValue);
    }
});

onMounted(() => {
    setModel(thing?.value);
})

const setModel = (thing: ThingDetailFragment | null | undefined) => {
    editModel.title = thing?.title
    editModel.typeId = thing?.type?.id;
    editModel.classId = thing?.class?.id
};

</script>
