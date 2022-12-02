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
    "\n    mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n        insertCorrespondent(input: $input) {\n            correspondent {\n                id\n                name\n            }\n        }\n    }\n": types.InsertCorrespondentDocument,
    "\n    fragment ThingItem on Thing {\n        id\n        title\n        type {\n            name\n        }\n        state\n        thumbnail(loadData: true, pageNumber: 1) {\n            url\n        }\n    }\n": types.ThingItemFragmentDoc,
    "\n    query getThingById($id: ID!) {\n        thingById(id: $id) {\n            id\n            ...ThingDetail\n        }\n    }\n\n    fragment ThingDetail on Thing {\n        id\n        title\n        type {\n            id\n            name\n        }\n        class {\n            id\n            name\n        }\n        source {\n            connectorId\n            connectorId\n        }\n        date\n        state\n        thumbnail(loadData: true, pageNumber: 1) {\n            url\n        }\n    }\n": types.GetThingByIdDocument,
    "\n    query thingsSearch {\n        things {\n            nodes {\n                id\n                ...ThingItem\n            }\n        }\n    }\n": types.ThingsSearchDocument,
    "query getThingByIdTest($id: ID!) {\n  thingById(id: $id) {\n    id\n    title\n    type {\n      name\n    }\n    source {\n      connectorId\n      connectorId\n    }\n    date\n    state\n    thumbnail(loadData: true, pageNumber: 1) {\n      url\n    }\n  }\n}": types.GetThingByIdTestDocument,
    "query lookups {\n  thingTypes {\n    id\n    name\n    validClasses {\n      id\n      name\n      properties {\n        id\n        name\n        dataType\n      }\n    }\n    contentTypeMap\n  }\n  correspondents {\n    nodes {\n      id\n      name\n    }\n  }\n}": types.LookupsDocument,
};

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n        insertCorrespondent(input: $input) {\n            correspondent {\n                id\n                name\n            }\n        }\n    }\n"): (typeof documents)["\n    mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n        insertCorrespondent(input: $input) {\n            correspondent {\n                id\n                name\n            }\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    fragment ThingItem on Thing {\n        id\n        title\n        type {\n            name\n        }\n        state\n        thumbnail(loadData: true, pageNumber: 1) {\n            url\n        }\n    }\n"): (typeof documents)["\n    fragment ThingItem on Thing {\n        id\n        title\n        type {\n            name\n        }\n        state\n        thumbnail(loadData: true, pageNumber: 1) {\n            url\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    query getThingById($id: ID!) {\n        thingById(id: $id) {\n            id\n            ...ThingDetail\n        }\n    }\n\n    fragment ThingDetail on Thing {\n        id\n        title\n        type {\n            id\n            name\n        }\n        class {\n            id\n            name\n        }\n        source {\n            connectorId\n            connectorId\n        }\n        date\n        state\n        thumbnail(loadData: true, pageNumber: 1) {\n            url\n        }\n    }\n"): (typeof documents)["\n    query getThingById($id: ID!) {\n        thingById(id: $id) {\n            id\n            ...ThingDetail\n        }\n    }\n\n    fragment ThingDetail on Thing {\n        id\n        title\n        type {\n            id\n            name\n        }\n        class {\n            id\n            name\n        }\n        source {\n            connectorId\n            connectorId\n        }\n        date\n        state\n        thumbnail(loadData: true, pageNumber: 1) {\n            url\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    query thingsSearch {\n        things {\n            nodes {\n                id\n                ...ThingItem\n            }\n        }\n    }\n"): (typeof documents)["\n    query thingsSearch {\n        things {\n            nodes {\n                id\n                ...ThingItem\n            }\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query getThingByIdTest($id: ID!) {\n  thingById(id: $id) {\n    id\n    title\n    type {\n      name\n    }\n    source {\n      connectorId\n      connectorId\n    }\n    date\n    state\n    thumbnail(loadData: true, pageNumber: 1) {\n      url\n    }\n  }\n}"): (typeof documents)["query getThingByIdTest($id: ID!) {\n  thingById(id: $id) {\n    id\n    title\n    type {\n      name\n    }\n    source {\n      connectorId\n      connectorId\n    }\n    date\n    state\n    thumbnail(loadData: true, pageNumber: 1) {\n      url\n    }\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query lookups {\n  thingTypes {\n    id\n    name\n    validClasses {\n      id\n      name\n      properties {\n        id\n        name\n        dataType\n      }\n    }\n    contentTypeMap\n  }\n  correspondents {\n    nodes {\n      id\n      name\n    }\n  }\n}"): (typeof documents)["query lookups {\n  thingTypes {\n    id\n    name\n    validClasses {\n      id\n      name\n      properties {\n        id\n        name\n        dataType\n      }\n    }\n    contentTypeMap\n  }\n  correspondents {\n    nodes {\n      id\n      name\n    }\n  }\n}"];

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