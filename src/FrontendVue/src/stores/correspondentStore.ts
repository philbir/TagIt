import { ref } from "vue";
import {
    SearchCorrespondentsDocument,
    InsertCorrespondentDocument,
} from "./../generated/graphql";
import { graphqlClient } from "./../graphqlClient";
import { defineStore } from "pinia";
import { pipe, subscribe } from "wonka";

export const useCorrenspondentStore = defineStore("correnspondent", () => {
    const list = ref();

    const loadAll = async () => {
        const { unsubscribe } = pipe(
            graphqlClient.query(SearchCorrespondentsDocument, {}),
            subscribe((result) => {
                list.value = result.data?.correspondents?.nodes;
            })
        );
    };

    const addCorrespondent = async (name: string) => {
        const result = await graphqlClient
            .mutation(InsertCorrespondentDocument, { input: { name } })
            .toPromise();

        return result.data?.insertCorrespondent.correspondent;
    };

    return { list, loadAll, addCorrespondent };
});
