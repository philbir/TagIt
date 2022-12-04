import { graphqlClient } from "./../graphqlClient";
import { LookupsDocument } from "./../generated/graphql";
import { ref } from "vue";
import { defineStore } from "pinia";

export const useLookupStore = defineStore("lookup", () => {
    const isReady = ref(false);
    const thingTypes = ref();
    const tagDefintions = ref();

    const load = async () => {
        const result = await graphqlClient
            .query(LookupsDocument, {})
            .toPromise();

        thingTypes.value = result.data?.thingTypes;
        tagDefintions.value = result.data?.tagDefintions?.nodes;

        isReady.value = true;
    };

    return { isReady, thingTypes,tagDefintions, load };
});
