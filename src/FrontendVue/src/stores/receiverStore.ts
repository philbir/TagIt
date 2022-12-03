import { ref } from "vue";
import { SearchReceiversDocument } from "../generated/graphql";
import { graphqlClient } from "../graphqlClient";
import { defineStore } from "pinia";
import { pipe, subscribe } from "wonka";

export const useReceiverStore = defineStore("receiver", () => {
    const list = ref();

    const loadAll = async () => {
        const { unsubscribe } = pipe(
            graphqlClient.query(SearchReceiversDocument, {}),
            subscribe((result) => {
                list.value = result.data?.receivers?.nodes;
            })
        );
    };

    return { list, loadAll };
});
