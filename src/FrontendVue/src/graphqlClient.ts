import { Client } from "@urql/vue";

export const graphqlClient = new Client({
    url: "/graphql",
    requestPolicy: "cache-first",
});
