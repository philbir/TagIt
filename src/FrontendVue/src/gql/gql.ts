/* eslint-disable */
import * as types from './graphql';
import type { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';

/**
 * Map of all GraphQL operations in the project.
 *
 * This map has several performance disadvantages:
 * 1. It is not tree-shakeable, so it will include all operations in the project.
 * 2. It is not minifiable, so the string of a GraphQL query will be multiple times inside the bundle.
 * 3. It does not support dead code elimination, so it will add unused operations.
 *
 * Therefore it is highly recommended to use the babel-plugin for production.
 */
const documents = {
    "\nfragment ThingItem on Thing {\n  id\n  title\n  type {\n    name\n  }\n  state\n  thumbnail(loadData: true, pageNumber: 1) {\n    url\n  }\n}\n": types.ThingItemFragmentDoc,
    "\n    query getThingById($id: ID!) {\n        thingById(id: $id) {\n            id\n            title\n            type {\n                name\n            }\n            source {\n                connectorId\n                connectorId\n            }\n            date\n            classId\n            state\n            thumbnail(loadData: true, pageNumber: 1) {\n                url\n            }\n        }\n    }\n": types.GetThingByIdDocument,
    "\n  query thingsSearch {\n    things {\n      nodes {\n        id\n        ...ThingItem\n      }\n    }\n  }\n": types.ThingsSearchDocument,
};

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\nfragment ThingItem on Thing {\n  id\n  title\n  type {\n    name\n  }\n  state\n  thumbnail(loadData: true, pageNumber: 1) {\n    url\n  }\n}\n"): (typeof documents)["\nfragment ThingItem on Thing {\n  id\n  title\n  type {\n    name\n  }\n  state\n  thumbnail(loadData: true, pageNumber: 1) {\n    url\n  }\n}\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    query getThingById($id: ID!) {\n        thingById(id: $id) {\n            id\n            title\n            type {\n                name\n            }\n            source {\n                connectorId\n                connectorId\n            }\n            date\n            classId\n            state\n            thumbnail(loadData: true, pageNumber: 1) {\n                url\n            }\n        }\n    }\n"): (typeof documents)["\n    query getThingById($id: ID!) {\n        thingById(id: $id) {\n            id\n            title\n            type {\n                name\n            }\n            source {\n                connectorId\n                connectorId\n            }\n            date\n            classId\n            state\n            thumbnail(loadData: true, pageNumber: 1) {\n                url\n            }\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  query thingsSearch {\n    things {\n      nodes {\n        id\n        ...ThingItem\n      }\n    }\n  }\n"): (typeof documents)["\n  query thingsSearch {\n    things {\n      nodes {\n        id\n        ...ThingItem\n      }\n    }\n  }\n"];

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 *
 *
 * @example
 * ```ts
 * const query = gql(`query GetUser($id: ID!) { user(id: $id) { name } }`);
 * ```
 *
 * The query argument is unknown!
 * Please regenerate the types.
**/
export function graphql(source: string): unknown;

export function graphql(source: string) {
  return (documents as any)[source] ?? {};
}

export type DocumentType<TDocumentNode extends DocumentNode<any, any>> = TDocumentNode extends DocumentNode<  infer TType,  any>  ? TType  : never;