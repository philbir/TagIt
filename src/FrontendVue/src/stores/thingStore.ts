import {
    UpdateThingDocument,
    UpdateThingInput,
    GetThingByIdDocument,
} from "./../generated/graphql";
import { graphqlClient } from "./../graphqlClient";
import { defineStore } from "pinia";

export const useThingStore = defineStore("things", () => {
    const getThingById = async (id: string) => {
        const result = await graphqlClient
            .query(GetThingByIdDocument, { id })
            .toPromise();
        return result?.data?.thingById;
    };

    const updateThing = async (input: UpdateThingInput) => {
        console.log("STORE", input);
        const result = await graphqlClient
            .mutation(UpdateThingDocument, {
                input,
            })
            .toPromise();
    };

    return { getThingById, updateThing };
});
