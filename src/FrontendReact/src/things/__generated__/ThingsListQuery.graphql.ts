/**
 * @generated SignedSource<<a31e44b11c7918be99a8aa94ce236b69>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
import { FragmentRefs } from "relay-runtime";
export type ThingsListQuery$variables = {};
export type ThingsListQuery$data = {
  readonly things: {
    readonly nodes: ReadonlyArray<{
      readonly id: string;
      readonly " $fragmentSpreads": FragmentRefs<"ThingItemCard_Thing">;
    }> | null;
  } | null;
};
export type ThingsListQuery = {
  response: ThingsListQuery$data;
  variables: ThingsListQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "id",
  "storageKey": null
};
return {
  "fragment": {
    "argumentDefinitions": [],
    "kind": "Fragment",
    "metadata": null,
    "name": "ThingsListQuery",
    "selections": [
      {
        "alias": null,
        "args": null,
        "concreteType": "ThingsConnection",
        "kind": "LinkedField",
        "name": "things",
        "plural": false,
        "selections": [
          {
            "alias": null,
            "args": null,
            "concreteType": "Thing",
            "kind": "LinkedField",
            "name": "nodes",
            "plural": true,
            "selections": [
              (v0/*: any*/),
              {
                "args": null,
                "kind": "FragmentSpread",
                "name": "ThingItemCard_Thing"
              }
            ],
            "storageKey": null
          }
        ],
        "storageKey": null
      }
    ],
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "ThingsListQuery",
    "selections": [
      {
        "alias": null,
        "args": null,
        "concreteType": "ThingsConnection",
        "kind": "LinkedField",
        "name": "things",
        "plural": false,
        "selections": [
          {
            "alias": null,
            "args": null,
            "concreteType": "Thing",
            "kind": "LinkedField",
            "name": "nodes",
            "plural": true,
            "selections": [
              (v0/*: any*/),
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "title",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "concreteType": "ThingType",
                "kind": "LinkedField",
                "name": "type",
                "plural": false,
                "selections": [
                  {
                    "alias": null,
                    "args": null,
                    "kind": "ScalarField",
                    "name": "name",
                    "storageKey": null
                  }
                ],
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "state",
                "storageKey": null
              }
            ],
            "storageKey": null
          }
        ],
        "storageKey": null
      }
    ]
  },
  "params": {
    "cacheID": "2a0a8462c968868b55342ab78c54954d",
    "id": null,
    "metadata": {},
    "name": "ThingsListQuery",
    "operationKind": "query",
    "text": "query ThingsListQuery {\n  things {\n    nodes {\n      id\n      ...ThingItemCard_Thing\n    }\n  }\n}\n\nfragment ThingItemCard_Thing on Thing {\n  id\n  title\n  type {\n    name\n  }\n  state\n}\n"
  }
};
})();

(node as any).hash = "9a634cc8a4087dd168b1266b7b4dea86";

export default node;
