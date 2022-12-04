<template>
    <v-progress-linear v-if="fetching" indeterminate></v-progress-linear>
    <div v-else>
        <v-toolbar>
            <v-toolbar-title>{{ editModel.title }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn
                variant="flat"
                color="success"
                class="mr-4"
                prepend-icon="mdi-content-save"
                @click="handleClickSave"
            >
                Save
            </v-btn>
        </v-toolbar>
        <v-sheet>
            <v-row>
                <v-col md="6">
                    <v-form>
                        <v-container>
                            <v-row>
                                <v-col xs="12">
                                    <v-text-field
                                        variant="solo"
                                        label="Title"
                                        v-model="editModel.title"
                                    ></v-text-field>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col xs="6">
                                    <v-autocomplete
                                        clearable
                                        label="Correspondent"
                                        :menu-props="{ maxHeight: 300 }"
                                        v-model="editModel.correspondentId"
                                        :items="correspondentStore.list"
                                        item-value="id"
                                        item-title="name"
                                        variant="solo"
                                        @update:search="
                                            handlecorrespondentSearch
                                        "
                                    >
                                        <template v-slot:no-data>
                                            <v-sheet class="ma-2">
                                                Add new
                                                {{ correspondentSearch }}
                                                <v-btn
                                                    @click="
                                                        handleClickAddCorrespondent
                                                    "
                                                    >Add</v-btn
                                                >
                                            </v-sheet>
                                        </template>
                                    </v-autocomplete>
                                </v-col>
                                <v-col>
                                    <v-select
                                        clearable
                                        variant="solo"
                                        label="Receiver"
                                        v-model="editModel.receiverId"
                                        item-value="id"
                                        item-title="name"
                                        :items="receiverStore.list"
                                    ></v-select>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col xs="6">
                                    <v-select
                                        label="Type"
                                        variant="solo"
                                        v-model="editModel.typeId"
                                        :items="lookupStore.thingTypes"
                                        item-value="id"
                                        item-title="name"
                                    ></v-select>
                                </v-col>
                                <v-col xs="6">
                                    <v-select
                                        v-if="classOptions.length > 0"
                                        label="Class"
                                        variant="solo"
                                        v-model="editModel.classId"
                                        :items="classOptions"
                                        item-value="id"
                                        item-title="name"
                                    ></v-select>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col xs="6">
                                    <v-autocomplete
                                        clearable
                                        label="Tags"
                                        :menu-props="{ maxHeight: 300 }"
                                        :items="lookupStore.tagDefintions"
                                        item-value="id"
                                        item-title="name"
                                        variant="solo"
                                        v-model="editModel.tags"
                                        multiple
                                        chips
                                        closable-chips
                                    >
                                        <template v-slot:no-data>
                                            <v-sheet class="ma-2">
                                                Add new Tag
                                                <v-btn>Add</v-btn>
                                            </v-sheet>
                                        </template>
                                        <template v-slot:chip="{ props, item }">
                                            <v-chip
                                                v-bind="props"
                                                :color="item.raw.color"
                                                :text="item.raw.name"
                                            ></v-chip>
                                        </template>
                                        <template v-slot:item="{ props, item }">
                                            <v-list-item
                                                v-bind="props"
                                                rounded="x1"
                                                title=""
                                            >
                                                <v-chip
                                                    :color="item.raw.color"
                                                    >{{ item.raw.name }}</v-chip
                                                >
                                            </v-list-item>
                                        </template>
                                    </v-autocomplete>
                                </v-col>
                            </v-row>
                            <v-row v-for="prop in properties">
                                <v-col>
                                    <ThingPropertyEdit
                                        :value="prop"
                                        @change="handlePropertyChange"
                                    />
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-form>
                </v-col>
                <v-col md="6" v-if="false">
                    <embed
                        width="100%"
                        :height="display.height.value - 80 + 'px'"
                        :src="pdfUrl"
                        id="plugin"
                    />
                </v-col>
                <v-col md="6" v-if="true">
                    <pre>
                                    {{ editModel }}
                                </pre
                    >
                    <hr />
                    <pre>
                                    {{ properties }}
                                </pre
                    >
                </v-col>
            </v-row>
        </v-sheet>
    </div>
</template>

<script setup lang="ts">
import { computed } from "@vue/reactivity";
import { useDisplay } from "vuetify/lib/framework.mjs";
import { useFragment } from "../generated";
import {
    ThingDetailFragment,
    ThingDetailFragmentDoc,
} from "../generated/graphql";
import { useLookupStore } from "@/stores/lookupStore";
import { useReceiverStore } from "@/stores/receiverStore";
import { useThingStore } from "@/stores/thingStore";
import { useCorrenspondentStore } from "@/stores/correspondentStore";
import { ref, reactive, onMounted } from "vue";
import ThingPropertyEdit from "./ThingPropertyEdit.vue";

const editModel = reactive<{
    id?: string | null;
    title?: string | null;
    typeId?: string | null;
    classId?: string | null;
    correspondentId?: string | null;
    receiverId?: string | null;
    tags?: Array<string> | null;
    properties: Array<{
        id?: string | null;
        definitionId?: string | null;
        value?: string | null;
        dataType?: string | null;
        name?: string | null;
    }>;
}>({
    properties: new Array(),
    tags: new Array(),
});

const display = useDisplay();
const lookupStore = useLookupStore();
const correspondentStore = useCorrenspondentStore();
const receiverStore = useReceiverStore();
const thingStore = useThingStore();

const correspondentSearch = ref();

// handlers
const handlecorrespondentSearch = (e: string) => {
    correspondentSearch.value = e;
};

const handleClickAddCorrespondent = async () => {
    const newCorrespondent = await correspondentStore.addCorrespondent(
        correspondentSearch.value
    );
    editModel.correspondentId = newCorrespondent?.id;
};

const handlePropertyChange = (e: any) => {
    if (e.id) {
        const existingProp = editModel.properties.find((x) => x.id === e.id);

        if (existingProp) {
            existingProp.value = e.value;
        } else {
            editModel.properties.push(e);
        }
    } else {
        const existingProp = editModel.properties.find(
            (x) => x.definitionId === e.definitionId
        );

        if (existingProp) {
            existingProp.value = e.value;
        } else {
            editModel.properties.push(e);
        }
    }
};

const handleClickSave = async () => {
    await thingStore.updateThing({
        id: props.id,
        title: editModel.title ?? "",
        typeId: editModel.typeId ?? "",
        classId: editModel.classId,
        receiverId: editModel.receiverId,
        correspondentId: editModel.correspondentId,
        properties: editModel.properties ?? [],
        tags: editModel.tags ?? [],
    });
};

const props = defineProps({
    id: {
        type: String,
        required: true,
    },
});

const classOptions = computed(() => {
    const types = lookupStore.thingTypes.filter(
        (x: any) => x.id === editModel.typeId
    );

    if (types.length > 0) {
        return types[0].validClasses;
    }
    return [];
});

const properties = computed(() => {
    const typeClasses = classOptions.value.filter(
        (x: any) => x.id === editModel.classId
    );

    const properties = [...editModel.properties].map((p) => {
        const def = thing.value?.properties?.find((x) => x?.id == p.id);
        return {
            id: p.id,
            value: p.value,
            dataType: def?.definition.dataType,
            definitionId: def?.id,
            name: def?.definition.name,
        };
    });

    //merge definition
    if (typeClasses.length > 0) {
        typeClasses[0].properties.forEach((p: any) => {
            const existing = editModel.properties.find(
                (x) => x.definitionId === p.id
            );

            if (!existing || !existing.id) {
                properties.push({
                    id: null,
                    definitionId: p.id,
                    value: "",
                    name: p.name,
                    dataType: p.dataType,
                });
            }
        });
    }

    return properties;
});

const pdfUrl = `/api/thing/preview/${props.id}/Archived`;

const thing = ref<ThingDetailFragment | null | undefined>();
const fetching = ref(true);

onMounted(async () => {
    const data = await thingStore.getThingById(props.id);
    thing.value = useFragment(ThingDetailFragmentDoc, data);
    fetching.value = false;

    if (thing.value) {
        setModel(thing.value);
    }
});

const setModel = (thing: ThingDetailFragment) => {
    console.log(thing.properties);
    editModel.title = thing.title;
    editModel.typeId = thing.type?.id;
    editModel.classId = thing.class?.id;
    editModel.receiverId = thing.receiver?.id;
    editModel.correspondentId = thing.correspondent?.id;
    editModel.tags = thing.tags.map((t) => t.id);
    editModel.properties = thing.properties?.map(
        (p: any) =>
            ({
                id: p.id,
                definitionId: p.definition.id,
                value: p.value,
            } ?? [])
    );
};
</script>
