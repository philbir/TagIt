import { graphqlClient } from './../graphqlClient';
import { LookupsDocument } from './../gql/graphql';
import { ref, computed } from 'vue'
import { defineStore } from 'pinia'



export const useLookupStore = defineStore('lookup', () => {

    const isReady = ref(false);
    const thingTypes = ref();

    const load = async () => {

        const result = await graphqlClient.query(LookupsDocument, {}).toPromise();
        thingTypes.value = result.data?.thingTypes;

        isReady.value = true;
    }

    return { isReady, thingTypes, load }
})
