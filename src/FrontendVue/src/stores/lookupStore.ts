import { graphqlClient } from "./../graphqlClient";
import {
    LookupsDocument,
    AddTagDefintionDocument,
} from "./../generated/graphql";
import { ref } from "vue";
import { defineStore } from "pinia";
import { pipe, subscribe } from "wonka";

export const useLookupStore = defineStore("lookup", () => {
    const isReady = ref(false);
    const thingTypes = ref();
    const tagDefintions = ref();

    const load = () => {
        const { unsubscribe } = pipe(
            graphqlClient.query(LookupsDocument, {}),
            subscribe((result) => {
                thingTypes.value = result.data?.thingTypes;
                tagDefintions.value = result.data?.tagDefintions?.nodes;
                isReady.value = true;
            })
        );
        /*
        const result = await graphqlClient
            .query(LookupsDocument, {})
            .toPromise();*/
    };

    const addTagDefintion = async (name: string) => {
        const result = await graphqlClient
            .mutation(AddTagDefintionDocument, { input: { name } })
            .toPromise();

        return result.data?.addTagDefintion.tagDefinition;
    };

    return { isReady, thingTypes, tagDefintions, load, addTagDefintion };
});
