import { Client } from '@urql/vue';

export const graphqlClient = new Client({
    url: 'http://localhost:5000/graphql',
    requestPolicy: 'cache-first',
});