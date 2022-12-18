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
    "\n    fragment ThingItem on Thing {\n        id\n        title\n        type {\n            name\n        }\n        state\n        thumbnail(loadData: false, pageNumber: 1) {\n            url\n        }\n    }\n": types.ThingItemFragmentDoc,
    "\n    query thingsSearch {\n        things {\n            nodes {\n                id\n                ...ThingItem\n            }\n        }\n    }\n": types.ThingsSearchDocument,
    "mutation addTagDefintion($input: AddTagDefintionInput!) {\n  addTagDefintion(input: $input) {\n    tagDefinition {\n      id\n      name\n      color\n    }\n  }\n}": types.AddTagDefintionDocument,
    "query getThingById($id: ID!) {\n  thingById(id: $id) {\n    id\n    ...ThingDetail\n  }\n}\n\nfragment ThingDetail on Thing {\n  id\n  title\n  type {\n    id\n    name\n  }\n  class {\n    id\n    name\n  }\n  correspondent {\n    id\n    name\n  }\n  receiver {\n    id\n    name\n  }\n  properties {\n    id\n    value\n    definition {\n      id\n      name\n      dataType\n    }\n  }\n  content {\n    text\n    detectedCorrespondents {\n      item {\n        id\n        name\n      }\n      scrore\n    }\n    tokens {\n      tokenizer\n      value\n      displays\n      matchCount\n    }\n  }\n  tags {\n    id\n    name\n    color\n  }\n  source {\n    connectorId\n    uniqueId\n  }\n  date\n  state\n  thumbnail(loadData: false, pageNumber: 1) {\n    url\n  }\n}": types.GetThingByIdDocument,
    "mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n  insertCorrespondent(input: $input) {\n    correspondent {\n      id\n      name\n    }\n  }\n}": types.InsertCorrespondentDocument,
    "query lookups {\n  thingTypes {\n    id\n    name\n    validClasses {\n      id\n      name\n      properties {\n        id\n        name\n        dataType\n      }\n    }\n    contentTypeMap\n  }\n  tagDefintions {\n    nodes {\n      id\n      name\n      color\n    }\n  }\n  thingStates\n}": types.LookupsDocument,
    "query searchCorrespondents {\n  correspondents(first: 50) {\n    nodes {\n      id\n      name\n    }\n  }\n}": types.SearchCorrespondentsDocument,
    "query searchReceivers {\n  receivers {\n    nodes {\n      id\n      name\n    }\n  }\n}": types.SearchReceiversDocument,
    "mutation updateThing($input: UpdateThingInput!) {\n  updateThing(input: $input) {\n    thing {\n      id\n    }\n  }\n}": types.UpdateThingDocument,
};

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n        insertCorrespondent(input: $input) {\n            correspondent {\n                id\n                name\n            }\n        }\n    }\n"): (typeof documents)["\n    mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n        insertCorrespondent(input: $input) {\n            correspondent {\n                id\n                name\n            }\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    fragment ThingItem on Thing {\n        id\n        title\n        type {\n            name\n        }\n        state\n        thumbnail(loadData: false, pageNumber: 1) {\n            url\n        }\n    }\n"): (typeof documents)["\n    fragment ThingItem on Thing {\n        id\n        title\n        type {\n            name\n        }\n        state\n        thumbnail(loadData: false, pageNumber: 1) {\n            url\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n    query thingsSearch {\n        things {\n            nodes {\n                id\n                ...ThingItem\n            }\n        }\n    }\n"): (typeof documents)["\n    query thingsSearch {\n        things {\n            nodes {\n                id\n                ...ThingItem\n            }\n        }\n    }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation addTagDefintion($input: AddTagDefintionInput!) {\n  addTagDefintion(input: $input) {\n    tagDefinition {\n      id\n      name\n      color\n    }\n  }\n}"): (typeof documents)["mutation addTagDefintion($input: AddTagDefintionInput!) {\n  addTagDefintion(input: $input) {\n    tagDefinition {\n      id\n      name\n      color\n    }\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query getThingById($id: ID!) {\n  thingById(id: $id) {\n    id\n    ...ThingDetail\n  }\n}\n\nfragment ThingDetail on Thing {\n  id\n  title\n  type {\n    id\n    name\n  }\n  class {\n    id\n    name\n  }\n  correspondent {\n    id\n    name\n  }\n  receiver {\n    id\n    name\n  }\n  properties {\n    id\n    value\n    definition {\n      id\n      name\n      dataType\n    }\n  }\n  content {\n    text\n    detectedCorrespondents {\n      item {\n        id\n        name\n      }\n      scrore\n    }\n    tokens {\n      tokenizer\n      value\n      displays\n      matchCount\n    }\n  }\n  tags {\n    id\n    name\n    color\n  }\n  source {\n    connectorId\n    uniqueId\n  }\n  date\n  state\n  thumbnail(loadData: false, pageNumber: 1) {\n    url\n  }\n}"): (typeof documents)["query getThingById($id: ID!) {\n  thingById(id: $id) {\n    id\n    ...ThingDetail\n  }\n}\n\nfragment ThingDetail on Thing {\n  id\n  title\n  type {\n    id\n    name\n  }\n  class {\n    id\n    name\n  }\n  correspondent {\n    id\n    name\n  }\n  receiver {\n    id\n    name\n  }\n  properties {\n    id\n    value\n    definition {\n      id\n      name\n      dataType\n    }\n  }\n  content {\n    text\n    detectedCorrespondents {\n      item {\n        id\n        name\n      }\n      scrore\n    }\n    tokens {\n      tokenizer\n      value\n      displays\n      matchCount\n    }\n  }\n  tags {\n    id\n    name\n    color\n  }\n  source {\n    connectorId\n    uniqueId\n  }\n  date\n  state\n  thumbnail(loadData: false, pageNumber: 1) {\n    url\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n  insertCorrespondent(input: $input) {\n    correspondent {\n      id\n      name\n    }\n  }\n}"): (typeof documents)["mutation insertCorrespondent($input: InsertCorrespondentInput!) {\n  insertCorrespondent(input: $input) {\n    correspondent {\n      id\n      name\n    }\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query lookups {\n  thingTypes {\n    id\n    name\n    validClasses {\n      id\n      name\n      properties {\n        id\n        name\n        dataType\n      }\n    }\n    contentTypeMap\n  }\n  tagDefintions {\n    nodes {\n      id\n      name\n      color\n    }\n  }\n  thingStates\n}"): (typeof documents)["query lookups {\n  thingTypes {\n    id\n    name\n    validClasses {\n      id\n      name\n      properties {\n        id\n        name\n        dataType\n      }\n    }\n    contentTypeMap\n  }\n  tagDefintions {\n    nodes {\n      id\n      name\n      color\n    }\n  }\n  thingStates\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query searchCorrespondents {\n  correspondents(first: 50) {\n    nodes {\n      id\n      name\n    }\n  }\n}"): (typeof documents)["query searchCorrespondents {\n  correspondents(first: 50) {\n    nodes {\n      id\n      name\n    }\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query searchReceivers {\n  receivers {\n    nodes {\n      id\n      name\n    }\n  }\n}"): (typeof documents)["query searchReceivers {\n  receivers {\n    nodes {\n      id\n      name\n    }\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation updateThing($input: UpdateThingInput!) {\n  updateThing(input: $input) {\n    thing {\n      id\n    }\n  }\n}"): (typeof documents)["mutation updateThing($input: UpdateThingInput!) {\n  updateThing(input: $input) {\n    thing {\n      id\n    }\n  }\n}"];

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