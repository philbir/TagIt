/**
 * @generated SignedSource<<5c8e3fef37aca7891be5efdab2a88f0e>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { Fragment, ReaderFragment } from 'relay-runtime';
export type ThingState = "ACTIVE" | "DELETED" | "DRAFT" | "PROCESSING" | "%future added value";
import { FragmentRefs } from "relay-runtime";
export type ThingItemCard_Thing$data = {
  readonly id: string;
  readonly state: ThingState;
  readonly title: string | null;
  readonly type: {
    readonly name: string | null;
  } | null;
  readonly " $fragmentType": "ThingItemCard_Thing";
};
export type ThingItemCard_Thing$key = {
  readonly " $data"?: ThingItemCard_Thing$data;
  readonly " $fragmentSpreads": FragmentRefs<"ThingItemCard_Thing">;
};

const node: ReaderFragment = {
  "argumentDefinitions": [],
  "kind": "Fragment",
  "metadata": null,
  "name": "ThingItemCard_Thing",
  "selections": [
    {
      "alias": null,
      "args": null,
      "kind": "ScalarField",
      "name": "id",
      "storageKey": null
    },
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
  "type": "Thing",
  "abstractKey": null
};

(node as any).hash = "1f31211d69d0da36e19e6efede75843c";

export default node;
