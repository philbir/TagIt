/* eslint-disable */
import type { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `Byte` scalar type represents non-fractional whole numeric values. Byte can represent values between 0 and 255. */
  Byte: any;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
  UUID: any;
};

export type AddOAuthCredentialClientInput = {
  client: OAuthClientInput;
  name: Scalars['String'];
};

export type AddOAuthCredentialClientPayload = {
  __typename?: 'AddOAuthCredentialClientPayload';
  credential?: Maybe<Credential>;
};

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER'
}

export type BooleanOperationFilterInput = {
  eq?: InputMaybe<Scalars['Boolean']>;
  neq?: InputMaybe<Scalars['Boolean']>;
};

export type ComparableByteOperationFilterInput = {
  eq?: InputMaybe<Scalars['Byte']>;
  gt?: InputMaybe<Scalars['Byte']>;
  gte?: InputMaybe<Scalars['Byte']>;
  in?: InputMaybe<Array<Scalars['Byte']>>;
  lt?: InputMaybe<Scalars['Byte']>;
  lte?: InputMaybe<Scalars['Byte']>;
  neq?: InputMaybe<Scalars['Byte']>;
  ngt?: InputMaybe<Scalars['Byte']>;
  ngte?: InputMaybe<Scalars['Byte']>;
  nin?: InputMaybe<Array<Scalars['Byte']>>;
  nlt?: InputMaybe<Scalars['Byte']>;
  nlte?: InputMaybe<Scalars['Byte']>;
};

export type ComparableDateTimeOffsetOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<Scalars['DateTime']>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<Scalars['DateTime']>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type ComparableDateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<Scalars['DateTime']>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<Scalars['DateTime']>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type ComparableGuidOperationFilterInput = {
  eq?: InputMaybe<Scalars['UUID']>;
  gt?: InputMaybe<Scalars['UUID']>;
  gte?: InputMaybe<Scalars['UUID']>;
  in?: InputMaybe<Array<Scalars['UUID']>>;
  lt?: InputMaybe<Scalars['UUID']>;
  lte?: InputMaybe<Scalars['UUID']>;
  neq?: InputMaybe<Scalars['UUID']>;
  ngt?: InputMaybe<Scalars['UUID']>;
  ngte?: InputMaybe<Scalars['UUID']>;
  nin?: InputMaybe<Array<Scalars['UUID']>>;
  nlt?: InputMaybe<Scalars['UUID']>;
  nlte?: InputMaybe<Scalars['UUID']>;
};

export type ComparableInt32OperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']>;
  gt?: InputMaybe<Scalars['Int']>;
  gte?: InputMaybe<Scalars['Int']>;
  in?: InputMaybe<Array<Scalars['Int']>>;
  lt?: InputMaybe<Scalars['Int']>;
  lte?: InputMaybe<Scalars['Int']>;
  neq?: InputMaybe<Scalars['Int']>;
  ngt?: InputMaybe<Scalars['Int']>;
  ngte?: InputMaybe<Scalars['Int']>;
  nin?: InputMaybe<Array<Scalars['Int']>>;
  nlt?: InputMaybe<Scalars['Int']>;
  nlte?: InputMaybe<Scalars['Int']>;
};

export type ComparableNullableOfDateTimeOffsetOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type ComparableNullableOfDateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type ComparableNullableOfGuidOperationFilterInput = {
  eq?: InputMaybe<Scalars['UUID']>;
  gt?: InputMaybe<Scalars['UUID']>;
  gte?: InputMaybe<Scalars['UUID']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['UUID']>>>;
  lt?: InputMaybe<Scalars['UUID']>;
  lte?: InputMaybe<Scalars['UUID']>;
  neq?: InputMaybe<Scalars['UUID']>;
  ngt?: InputMaybe<Scalars['UUID']>;
  ngte?: InputMaybe<Scalars['UUID']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['UUID']>>>;
  nlt?: InputMaybe<Scalars['UUID']>;
  nlte?: InputMaybe<Scalars['UUID']>;
};

export type ComparableNullableOfInt32OperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']>;
  gt?: InputMaybe<Scalars['Int']>;
  gte?: InputMaybe<Scalars['Int']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']>>>;
  lt?: InputMaybe<Scalars['Int']>;
  lte?: InputMaybe<Scalars['Int']>;
  neq?: InputMaybe<Scalars['Int']>;
  ngt?: InputMaybe<Scalars['Int']>;
  ngte?: InputMaybe<Scalars['Int']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']>>>;
  nlt?: InputMaybe<Scalars['Int']>;
  nlte?: InputMaybe<Scalars['Int']>;
};

export type ConnectorDefintion = Node & {
  __typename?: 'ConnectorDefintion';
  credential?: Maybe<Credential>;
  credentialId?: Maybe<Scalars['ID']>;
  id: Scalars['ID'];
  name?: Maybe<Scalars['String']>;
  properties?: Maybe<Array<KeyValuePairOfStringAndString>>;
  root?: Maybe<Scalars['String']>;
  type?: Maybe<Scalars['String']>;
};

export type ConnectorDefintionFilterInput = {
  and?: InputMaybe<Array<ConnectorDefintionFilterInput>>;
  credentialId?: InputMaybe<IdOperationFilterInput>;
  id?: InputMaybe<IdOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ConnectorDefintionFilterInput>>;
  properties?: InputMaybe<IDictionaryOfStringAndStringFilterInput>;
  root?: InputMaybe<StringOperationFilterInput>;
  type?: InputMaybe<StringOperationFilterInput>;
};

/** A connection to a list of items. */
export type ConnectorsConnection = {
  __typename?: 'ConnectorsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<ConnectorsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<ConnectorDefintion>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type ConnectorsEdge = {
  __typename?: 'ConnectorsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: ConnectorDefintion;
};

export type Correspondent = Node & {
  __typename?: 'Correspondent';
  id: Scalars['ID'];
  name?: Maybe<Scalars['String']>;
};

export type CorrespondentFilterInput = {
  and?: InputMaybe<Array<CorrespondentFilterInput>>;
  id?: InputMaybe<ComparableGuidOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<CorrespondentFilterInput>>;
};

/** A connection to a list of items. */
export type CorrespondentsConnection = {
  __typename?: 'CorrespondentsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<CorrespondentsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Correspondent>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type CorrespondentsEdge = {
  __typename?: 'CorrespondentsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Correspondent;
};

export type Credential = {
  __typename?: 'Credential';
  client?: Maybe<OAuthClient>;
  id: Scalars['ID'];
  name?: Maybe<Scalars['String']>;
  tokens?: Maybe<Array<Maybe<CredentialToken>>>;
};

export type CredentialFilterInput = {
  and?: InputMaybe<Array<CredentialFilterInput>>;
  client?: InputMaybe<OAuthClientFilterInput>;
  id?: InputMaybe<IdOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<CredentialFilterInput>>;
  tokens?: InputMaybe<ListFilterInputTypeOfCredentialTokenFilterInput>;
};

export type CredentialToken = {
  __typename?: 'CredentialToken';
  createdAt: Scalars['DateTime'];
  expiresAt?: Maybe<Scalars['DateTime']>;
  id: Scalars['UUID'];
  type: TokenType;
  value?: Maybe<Scalars['String']>;
};

export type CredentialTokenFilterInput = {
  and?: InputMaybe<Array<CredentialTokenFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOffsetOperationFilterInput>;
  expiresAt?: InputMaybe<ComparableNullableOfDateTimeOffsetOperationFilterInput>;
  id?: InputMaybe<ComparableGuidOperationFilterInput>;
  or?: InputMaybe<Array<CredentialTokenFilterInput>>;
  type?: InputMaybe<TokenTypeOperationFilterInput>;
  value?: InputMaybe<ProtectedValueFilterInput>;
};

/** A connection to a list of items. */
export type CredentialsConnection = {
  __typename?: 'CredentialsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<CredentialsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Credential>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type CredentialsEdge = {
  __typename?: 'CredentialsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Credential;
};

export type EntityVersion = {
  __typename?: 'EntityVersion';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['UUID'];
  version: Scalars['Int'];
};

export type EntityVersionFilterInput = {
  and?: InputMaybe<Array<EntityVersionFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  createdBy?: InputMaybe<ComparableGuidOperationFilterInput>;
  or?: InputMaybe<Array<EntityVersionFilterInput>>;
  version?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type IDictionaryOfStringAndStringFilterInput = {
  and?: InputMaybe<Array<IDictionaryOfStringAndStringFilterInput>>;
  keys?: InputMaybe<ListStringOperationFilterInput>;
  or?: InputMaybe<Array<IDictionaryOfStringAndStringFilterInput>>;
  values?: InputMaybe<ListStringOperationFilterInput>;
};

export type IdOperationFilterInput = {
  eq?: InputMaybe<Scalars['ID']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['ID']>>>;
  neq?: InputMaybe<Scalars['ID']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['ID']>>>;
};

export enum ImageFormat {
  Png = 'PNG',
  WebP = 'WEB_P'
}

export type ImageFormatOperationFilterInput = {
  eq?: InputMaybe<ImageFormat>;
  in?: InputMaybe<Array<ImageFormat>>;
  neq?: InputMaybe<ImageFormat>;
  nin?: InputMaybe<Array<ImageFormat>>;
};

export type ImageSize = {
  __typename?: 'ImageSize';
  height: Scalars['Int'];
  width: Scalars['Int'];
};

export type ImageSizeFilterInput = {
  and?: InputMaybe<Array<ImageSizeFilterInput>>;
  height?: InputMaybe<ComparableInt32OperationFilterInput>;
  or?: InputMaybe<Array<ImageSizeFilterInput>>;
  width?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type InsertCorrespondentInput = {
  name: Scalars['String'];
};

export type InsertCorrespondentPayload = {
  __typename?: 'InsertCorrespondentPayload';
  correspondent?: Maybe<Correspondent>;
};

export type JobAction = {
  __typename?: 'JobAction';
  destinationConnector?: Maybe<ConnectorDefintion>;
  destinationConnectorId?: Maybe<Scalars['ID']>;
  mode: JobActionMode;
  source?: Maybe<SourceAction>;
};

export type JobActionFilterInput = {
  and?: InputMaybe<Array<JobActionFilterInput>>;
  destinationConnectorId?: InputMaybe<IdOperationFilterInput>;
  mode?: InputMaybe<JobActionModeOperationFilterInput>;
  or?: InputMaybe<Array<JobActionFilterInput>>;
  source?: InputMaybe<SourceActionFilterInput>;
};

export type JobActionInput = {
  destinationConnectorId?: InputMaybe<Scalars['ID']>;
  mode: JobActionMode;
  source?: InputMaybe<SourceActionInput>;
};

export enum JobActionMode {
  Import = 'IMPORT',
  Link = 'LINK'
}

export type JobActionModeOperationFilterInput = {
  eq?: InputMaybe<JobActionMode>;
  in?: InputMaybe<Array<JobActionMode>>;
  neq?: InputMaybe<JobActionMode>;
  nin?: InputMaybe<Array<JobActionMode>>;
};

export type JobDefintion = Node & {
  __typename?: 'JobDefintion';
  action: JobAction;
  cronSchedule?: Maybe<Scalars['String']>;
  enabled: Scalars['Boolean'];
  filter?: Maybe<Scalars['String']>;
  id: Scalars['ID'];
  name: Scalars['String'];
  runMode: JobRunMode;
  schedule?: Maybe<JobSchedule>;
  sourceConnector: ConnectorDefintion;
  sourceConnectorId: Scalars['ID'];
};

export type JobDefintionFilterInput = {
  action?: InputMaybe<JobActionFilterInput>;
  and?: InputMaybe<Array<JobDefintionFilterInput>>;
  cronSchedule?: InputMaybe<StringOperationFilterInput>;
  enabled?: InputMaybe<BooleanOperationFilterInput>;
  filter?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<IdOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<JobDefintionFilterInput>>;
  runMode?: InputMaybe<JobRunModeOperationFilterInput>;
  schedule?: InputMaybe<JobScheduleFilterInput>;
  sourceConnectorId?: InputMaybe<IdOperationFilterInput>;
};

/** A connection to a list of items. */
export type JobDefintionsConnection = {
  __typename?: 'JobDefintionsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<JobDefintionsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<JobDefintion>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type JobDefintionsEdge = {
  __typename?: 'JobDefintionsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: JobDefintion;
};

export enum JobRunMode {
  Harvest = 'HARVEST',
  Watch = 'WATCH'
}

export type JobRunModeOperationFilterInput = {
  eq?: InputMaybe<JobRunMode>;
  in?: InputMaybe<Array<JobRunMode>>;
  neq?: InputMaybe<JobRunMode>;
  nin?: InputMaybe<Array<JobRunMode>>;
};

export type JobSchedule = {
  __typename?: 'JobSchedule';
  cronExpression?: Maybe<Scalars['String']>;
  intervall?: Maybe<Scalars['Int']>;
  type: JobScheduleType;
};

export type JobScheduleFilterInput = {
  and?: InputMaybe<Array<JobScheduleFilterInput>>;
  cronExpression?: InputMaybe<StringOperationFilterInput>;
  intervall?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  or?: InputMaybe<Array<JobScheduleFilterInput>>;
  type?: InputMaybe<JobScheduleTypeOperationFilterInput>;
};

export type JobScheduleInput = {
  cronExpression?: InputMaybe<Scalars['String']>;
  intervall?: InputMaybe<Scalars['Int']>;
  type: JobScheduleType;
};

export enum JobScheduleType {
  Cron = 'CRON',
  Interval = 'INTERVAL'
}

export type JobScheduleTypeOperationFilterInput = {
  eq?: InputMaybe<JobScheduleType>;
  in?: InputMaybe<Array<JobScheduleType>>;
  neq?: InputMaybe<JobScheduleType>;
  nin?: InputMaybe<Array<JobScheduleType>>;
};

export type KeyValuePairOfStringAndString = {
  __typename?: 'KeyValuePairOfStringAndString';
  key: Scalars['String'];
  value: Scalars['String'];
};

export type ListComparableByteOperationFilterInput = {
  all?: InputMaybe<ComparableByteOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ComparableByteOperationFilterInput>;
  some?: InputMaybe<ComparableByteOperationFilterInput>;
};

export type ListComparableGuidOperationFilterInput = {
  all?: InputMaybe<ComparableGuidOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ComparableGuidOperationFilterInput>;
  some?: InputMaybe<ComparableGuidOperationFilterInput>;
};

export type ListFilterInputTypeOfCredentialTokenFilterInput = {
  all?: InputMaybe<CredentialTokenFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<CredentialTokenFilterInput>;
  some?: InputMaybe<CredentialTokenFilterInput>;
};

export type ListFilterInputTypeOfPropertyDefinitionLinkFilterInput = {
  all?: InputMaybe<PropertyDefinitionLinkFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<PropertyDefinitionLinkFilterInput>;
  some?: InputMaybe<PropertyDefinitionLinkFilterInput>;
};

export type ListFilterInputTypeOfTagFilterInput = {
  all?: InputMaybe<TagFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<TagFilterInput>;
  some?: InputMaybe<TagFilterInput>;
};

export type ListFilterInputTypeOfThingDataReferenceFilterInput = {
  all?: InputMaybe<ThingDataReferenceFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ThingDataReferenceFilterInput>;
  some?: InputMaybe<ThingDataReferenceFilterInput>;
};

export type ListFilterInputTypeOfThingProperyFilterInput = {
  all?: InputMaybe<ThingProperyFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ThingProperyFilterInput>;
  some?: InputMaybe<ThingProperyFilterInput>;
};

export type ListFilterInputTypeOfThingRelationFilterInput = {
  all?: InputMaybe<ThingRelationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ThingRelationFilterInput>;
  some?: InputMaybe<ThingRelationFilterInput>;
};

export type ListFilterInputTypeOfThingThumbnailFilterInput = {
  all?: InputMaybe<ThingThumbnailFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ThingThumbnailFilterInput>;
  some?: InputMaybe<ThingThumbnailFilterInput>;
};

export type ListStringOperationFilterInput = {
  all?: InputMaybe<StringOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<StringOperationFilterInput>;
  some?: InputMaybe<StringOperationFilterInput>;
};

export type Mutation = {
  __typename?: 'Mutation';
  addOAuthCredentialClient: AddOAuthCredentialClientPayload;
  insertCorrespondent: InsertCorrespondentPayload;
  updateJobDefinition: UpdateJobDefinitionPayload;
};


export type MutationAddOAuthCredentialClientArgs = {
  input: AddOAuthCredentialClientInput;
};


export type MutationInsertCorrespondentArgs = {
  input: InsertCorrespondentInput;
};


export type MutationUpdateJobDefinitionArgs = {
  input: UpdateJobDefinitionInput;
};

/** The node interface is implemented by entities that have a global unique identifier. */
export type Node = {
  id: Scalars['ID'];
};

export type OAuthClient = {
  __typename?: 'OAuthClient';
  authority?: Maybe<Scalars['String']>;
  flow: OAuthFlow;
  id?: Maybe<Scalars['String']>;
  product?: Maybe<Scalars['String']>;
  scopes?: Maybe<Array<Maybe<Scalars['String']>>>;
  secret?: Maybe<Scalars['String']>;
};

export type OAuthClientFilterInput = {
  and?: InputMaybe<Array<OAuthClientFilterInput>>;
  authority?: InputMaybe<StringOperationFilterInput>;
  flow?: InputMaybe<OAuthFlowOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<OAuthClientFilterInput>>;
  product?: InputMaybe<StringOperationFilterInput>;
  scopes?: InputMaybe<ListStringOperationFilterInput>;
  secret?: InputMaybe<ProtectedValueFilterInput>;
};

export type OAuthClientInput = {
  authority?: InputMaybe<Scalars['String']>;
  flow: OAuthFlow;
  id?: InputMaybe<Scalars['String']>;
  product?: InputMaybe<Scalars['String']>;
  scopes?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  secret?: InputMaybe<ProtectedValueInput>;
};

export enum OAuthFlow {
  ClientCredentials = 'CLIENT_CREDENTIALS',
  Code = 'CODE',
  Device = 'DEVICE'
}

export type OAuthFlowOperationFilterInput = {
  eq?: InputMaybe<OAuthFlow>;
  in?: InputMaybe<Array<OAuthFlow>>;
  neq?: InputMaybe<OAuthFlow>;
  nin?: InputMaybe<Array<OAuthFlow>>;
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']>;
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']>;
};

export enum PropertyDataType {
  Boolean = 'BOOLEAN',
  DateTime = 'DATE_TIME',
  Number = 'NUMBER',
  String = 'STRING'
}

export type PropertyDefinition = {
  __typename?: 'PropertyDefinition';
  dataType: PropertyDataType;
  id: Scalars['ID'];
  name?: Maybe<Scalars['String']>;
  options?: Maybe<Array<Maybe<Scalars['String']>>>;
};

export type PropertyDefinitionLinkFilterInput = {
  and?: InputMaybe<Array<PropertyDefinitionLinkFilterInput>>;
  definitionId?: InputMaybe<IdOperationFilterInput>;
  or?: InputMaybe<Array<PropertyDefinitionLinkFilterInput>>;
};

export type ProtectedValue = {
  __typename?: 'ProtectedValue';
  cipher?: Maybe<Array<Scalars['Byte']>>;
};

export type ProtectedValueFilterInput = {
  and?: InputMaybe<Array<ProtectedValueFilterInput>>;
  cipher?: InputMaybe<ListComparableByteOperationFilterInput>;
  or?: InputMaybe<Array<ProtectedValueFilterInput>>;
  value?: InputMaybe<StringOperationFilterInput>;
};

export type ProtectedValueInput = {
  cipher?: InputMaybe<Array<Scalars['Byte']>>;
  value?: InputMaybe<Scalars['String']>;
};

export type Query = {
  __typename?: 'Query';
  connectors?: Maybe<ConnectorsConnection>;
  correspondentById: Correspondent;
  correspondents?: Maybe<CorrespondentsConnection>;
  credentials?: Maybe<CredentialsConnection>;
  credentialsById: Credential;
  jobDefinitionById: JobDefintion;
  jobDefintions?: Maybe<JobDefintionsConnection>;
  /** Fetches an object given its ID. */
  node?: Maybe<Node>;
  /** Lookup nodes by a list of IDs. */
  nodes: Array<Maybe<Node>>;
  thingById: Thing;
  thingClasses: Array<ThingClass>;
  thingTypes: Array<ThingType>;
  things?: Maybe<ThingsConnection>;
  webHooks: Array<WebHook>;
};


export type QueryConnectorsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  where?: InputMaybe<ConnectorDefintionFilterInput>;
};


export type QueryCorrespondentByIdArgs = {
  id: Scalars['ID'];
};


export type QueryCorrespondentsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  where?: InputMaybe<CorrespondentFilterInput>;
};


export type QueryCredentialsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  where?: InputMaybe<CredentialFilterInput>;
};


export type QueryCredentialsByIdArgs = {
  id: Scalars['ID'];
};


export type QueryJobDefinitionByIdArgs = {
  id: Scalars['ID'];
};


export type QueryJobDefintionsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  where?: InputMaybe<JobDefintionFilterInput>;
};


export type QueryNodeArgs = {
  id: Scalars['ID'];
};


export type QueryNodesArgs = {
  ids: Array<Scalars['ID']>;
};


export type QueryThingByIdArgs = {
  id: Scalars['ID'];
};


export type QueryThingClassesArgs = {
  where?: InputMaybe<ThingClassFilterInput>;
};


export type QueryThingTypesArgs = {
  where?: InputMaybe<ThingTypeFilterInput>;
};


export type QueryThingsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  where?: InputMaybe<ThingFilterInput>;
};

export enum RelationType {
  Child = 'CHILD',
  Relatated = 'RELATATED'
}

export type RelationTypeOperationFilterInput = {
  eq?: InputMaybe<RelationType>;
  in?: InputMaybe<Array<RelationType>>;
  neq?: InputMaybe<RelationType>;
  nin?: InputMaybe<Array<RelationType>>;
};

export type SourceAction = {
  __typename?: 'SourceAction';
  mode: SourceActionMode;
  newConnectorId?: Maybe<Scalars['ID']>;
  newLocation?: Maybe<Scalars['String']>;
};

export type SourceActionFilterInput = {
  and?: InputMaybe<Array<SourceActionFilterInput>>;
  mode?: InputMaybe<SourceActionModeOperationFilterInput>;
  newConnectorId?: InputMaybe<IdOperationFilterInput>;
  newLocation?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<SourceActionFilterInput>>;
};

export type SourceActionInput = {
  mode: SourceActionMode;
  newConnectorId?: InputMaybe<Scalars['ID']>;
  newLocation?: InputMaybe<Scalars['String']>;
};

export enum SourceActionMode {
  Delete = 'DELETE',
  Move = 'MOVE'
}

export type SourceActionModeOperationFilterInput = {
  eq?: InputMaybe<SourceActionMode>;
  in?: InputMaybe<Array<SourceActionMode>>;
  neq?: InputMaybe<SourceActionMode>;
  nin?: InputMaybe<Array<SourceActionMode>>;
};

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']>;
  endsWith?: InputMaybe<Scalars['String']>;
  eq?: InputMaybe<Scalars['String']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  ncontains?: InputMaybe<Scalars['String']>;
  nendsWith?: InputMaybe<Scalars['String']>;
  neq?: InputMaybe<Scalars['String']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  nstartsWith?: InputMaybe<Scalars['String']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']>;
};

export type Tag = {
  __typename?: 'Tag';
  text?: Maybe<Scalars['String']>;
  typeId: Scalars['UUID'];
  value?: Maybe<Scalars['String']>;
};

export type TagFilterInput = {
  and?: InputMaybe<Array<TagFilterInput>>;
  or?: InputMaybe<Array<TagFilterInput>>;
  text?: InputMaybe<StringOperationFilterInput>;
  typeId?: InputMaybe<ComparableGuidOperationFilterInput>;
  value?: InputMaybe<StringOperationFilterInput>;
};

export type Thing = Node & {
  __typename?: 'Thing';
  classId?: Maybe<Scalars['ID']>;
  corespondentId?: Maybe<Scalars['ID']>;
  date?: Maybe<Scalars['DateTime']>;
  id: Scalars['ID'];
  label?: Maybe<Scalars['String']>;
  properties?: Maybe<Array<Maybe<ThingPropery>>>;
  receiverId?: Maybe<Scalars['ID']>;
  relations?: Maybe<Array<Maybe<ThingRelation>>>;
  source?: Maybe<ThingSource>;
  state: ThingState;
  tags?: Maybe<Array<Maybe<Tag>>>;
  thumbnail?: Maybe<ThingThumbnail>;
  title?: Maybe<Scalars['String']>;
  type?: Maybe<ThingType>;
  typeId?: Maybe<Scalars['ID']>;
  version: EntityVersion;
};


export type ThingThumbnailArgs = {
  loadData?: Scalars['Boolean'];
  pageNumber?: Scalars['Int'];
};

export type ThingClass = {
  __typename?: 'ThingClass';
  id: Scalars['ID'];
  name?: Maybe<Scalars['String']>;
  properties: Array<PropertyDefinition>;
  version: EntityVersion;
};

export type ThingClassFilterInput = {
  and?: InputMaybe<Array<ThingClassFilterInput>>;
  id?: InputMaybe<IdOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ThingClassFilterInput>>;
  properties?: InputMaybe<ListFilterInputTypeOfPropertyDefinitionLinkFilterInput>;
  version?: InputMaybe<EntityVersionFilterInput>;
};

export type ThingDataReferenceFilterInput = {
  and?: InputMaybe<Array<ThingDataReferenceFilterInput>>;
  connectorId?: InputMaybe<ComparableGuidOperationFilterInput>;
  contentType?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ThingDataReferenceFilterInput>>;
  type?: InputMaybe<StringOperationFilterInput>;
};

export type ThingFilterInput = {
  and?: InputMaybe<Array<ThingFilterInput>>;
  classId?: InputMaybe<ComparableNullableOfGuidOperationFilterInput>;
  corespondentId?: InputMaybe<ComparableNullableOfGuidOperationFilterInput>;
  data?: InputMaybe<ListFilterInputTypeOfThingDataReferenceFilterInput>;
  date?: InputMaybe<ComparableNullableOfDateTimeOperationFilterInput>;
  id?: InputMaybe<IdOperationFilterInput>;
  label?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ThingFilterInput>>;
  properties?: InputMaybe<ListFilterInputTypeOfThingProperyFilterInput>;
  receiverId?: InputMaybe<ComparableNullableOfGuidOperationFilterInput>;
  relations?: InputMaybe<ListFilterInputTypeOfThingRelationFilterInput>;
  source?: InputMaybe<ThingSourceFilterInput>;
  state?: InputMaybe<ThingStateOperationFilterInput>;
  tags?: InputMaybe<ListFilterInputTypeOfTagFilterInput>;
  thumbnails?: InputMaybe<ListFilterInputTypeOfThingThumbnailFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  typeId?: InputMaybe<ComparableNullableOfGuidOperationFilterInput>;
  version?: InputMaybe<EntityVersionFilterInput>;
};

export type ThingPropery = {
  __typename?: 'ThingPropery';
  defintionId: Scalars['ID'];
  id: Scalars['UUID'];
  value?: Maybe<Scalars['String']>;
};

export type ThingProperyFilterInput = {
  and?: InputMaybe<Array<ThingProperyFilterInput>>;
  defintionId?: InputMaybe<IdOperationFilterInput>;
  id?: InputMaybe<ComparableGuidOperationFilterInput>;
  or?: InputMaybe<Array<ThingProperyFilterInput>>;
  value?: InputMaybe<StringOperationFilterInput>;
};

export type ThingRelation = {
  __typename?: 'ThingRelation';
  from: Scalars['UUID'];
  id: Scalars['UUID'];
  to: Scalars['UUID'];
  type: RelationType;
};

export type ThingRelationFilterInput = {
  and?: InputMaybe<Array<ThingRelationFilterInput>>;
  from?: InputMaybe<ComparableGuidOperationFilterInput>;
  id?: InputMaybe<ComparableGuidOperationFilterInput>;
  or?: InputMaybe<Array<ThingRelationFilterInput>>;
  to?: InputMaybe<ComparableGuidOperationFilterInput>;
  type?: InputMaybe<RelationTypeOperationFilterInput>;
};

export type ThingSource = {
  __typename?: 'ThingSource';
  connectorId: Scalars['UUID'];
  id?: Maybe<Scalars['String']>;
  uniqueId?: Maybe<Scalars['String']>;
};

export type ThingSourceFilterInput = {
  and?: InputMaybe<Array<ThingSourceFilterInput>>;
  connectorId?: InputMaybe<ComparableGuidOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ThingSourceFilterInput>>;
  uniqueId?: InputMaybe<StringOperationFilterInput>;
};

export enum ThingState {
  Active = 'ACTIVE',
  Deleted = 'DELETED',
  Draft = 'DRAFT',
  Processing = 'PROCESSING'
}

export type ThingStateOperationFilterInput = {
  eq?: InputMaybe<ThingState>;
  in?: InputMaybe<Array<ThingState>>;
  neq?: InputMaybe<ThingState>;
  nin?: InputMaybe<Array<ThingState>>;
};

export type ThingThumbnail = {
  __typename?: 'ThingThumbnail';
  format: ImageFormat;
  pageNumber: Scalars['Int'];
  size?: Maybe<ImageSize>;
  url: Scalars['String'];
};

export type ThingThumbnailFilterInput = {
  and?: InputMaybe<Array<ThingThumbnailFilterInput>>;
  data?: InputMaybe<ListComparableByteOperationFilterInput>;
  fileId?: InputMaybe<StringOperationFilterInput>;
  format?: InputMaybe<ImageFormatOperationFilterInput>;
  or?: InputMaybe<Array<ThingThumbnailFilterInput>>;
  pageNumber?: InputMaybe<ComparableInt32OperationFilterInput>;
  size?: InputMaybe<ImageSizeFilterInput>;
};

export type ThingType = {
  __typename?: 'ThingType';
  contentTypeMap?: Maybe<Array<Maybe<Scalars['String']>>>;
  id: Scalars['ID'];
  name?: Maybe<Scalars['String']>;
  validClasses?: Maybe<Array<Scalars['UUID']>>;
  version: EntityVersion;
};

export type ThingTypeFilterInput = {
  and?: InputMaybe<Array<ThingTypeFilterInput>>;
  contentTypeMap?: InputMaybe<ListStringOperationFilterInput>;
  id?: InputMaybe<IdOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ThingTypeFilterInput>>;
  validClasses?: InputMaybe<ListComparableGuidOperationFilterInput>;
  version?: InputMaybe<EntityVersionFilterInput>;
};

/** A connection to a list of items. */
export type ThingsConnection = {
  __typename?: 'ThingsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<ThingsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Thing>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type ThingsEdge = {
  __typename?: 'ThingsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Thing;
};

export enum TokenType {
  Access = 'ACCESS',
  Id = 'ID',
  Refresh = 'REFRESH'
}

export type TokenTypeOperationFilterInput = {
  eq?: InputMaybe<TokenType>;
  in?: InputMaybe<Array<TokenType>>;
  neq?: InputMaybe<TokenType>;
  nin?: InputMaybe<Array<TokenType>>;
};

export type UpdateJobDefinitionInput = {
  action: JobActionInput;
  enabled: Scalars['Boolean'];
  filter?: InputMaybe<Scalars['String']>;
  id: Scalars['ID'];
  name: Scalars['String'];
  runMode: JobRunMode;
  schedule?: InputMaybe<JobScheduleInput>;
  sourceConnectorId: Scalars['ID'];
};

export type UpdateJobDefinitionPayload = {
  __typename?: 'UpdateJobDefinitionPayload';
  jobDefintion?: Maybe<JobDefintion>;
};

export type UserError = {
  code: Scalars['String'];
  message: Scalars['String'];
};

export type WebHook = {
  __typename?: 'WebHook';
  clientState: Scalars['String'];
  connectorId: Scalars['UUID'];
  createdAt: Scalars['DateTime'];
  expiresAt?: Maybe<Scalars['DateTime']>;
  id: Scalars['UUID'];
  identifier: Scalars['String'];
  jobId: Scalars['UUID'];
  product: Scalars['String'];
  url: Scalars['String'];
};

export type InsertCorrespondentMutationVariables = Exact<{
  input: InsertCorrespondentInput;
}>;


export type InsertCorrespondentMutation = { __typename?: 'Mutation', insertCorrespondent: { __typename?: 'InsertCorrespondentPayload', correspondent?: { __typename?: 'Correspondent', id: string, name?: string | null } | null } };

export type ThingItemFragment = { __typename?: 'Thing', id: string, title?: string | null, state: ThingState, type?: { __typename?: 'ThingType', name?: string | null } | null, thumbnail?: { __typename?: 'ThingThumbnail', url: string } | null } & { ' $fragmentName'?: 'ThingItemFragment' };

export type GetThingByIdQueryVariables = Exact<{
  id: Scalars['ID'];
}>;


export type GetThingByIdQuery = { __typename?: 'Query', thingById: (
    { __typename?: 'Thing', id: string }
    & { ' $fragmentRefs'?: { 'ThingDetailFragment': ThingDetailFragment } }
  ) };

export type ThingDetailFragment = { __typename?: 'Thing', id: string, title?: string | null, date?: any | null, classId?: string | null, state: ThingState, type?: { __typename?: 'ThingType', name?: string | null } | null, source?: { __typename?: 'ThingSource', connectorId: any } | null, thumbnail?: { __typename?: 'ThingThumbnail', url: string } | null } & { ' $fragmentName'?: 'ThingDetailFragment' };

export type ThingsSearchQueryVariables = Exact<{ [key: string]: never; }>;


export type ThingsSearchQuery = { __typename?: 'Query', things?: { __typename?: 'ThingsConnection', nodes?: Array<(
      { __typename?: 'Thing' }
      & { ' $fragmentRefs'?: { 'ThingItemFragment': ThingItemFragment } }
    )> | null } | null };

export const ThingItemFragmentDoc = {"kind":"Document","definitions":[{"kind":"FragmentDefinition","name":{"kind":"Name","value":"ThingItem"},"typeCondition":{"kind":"NamedType","name":{"kind":"Name","value":"Thing"}},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"title"}},{"kind":"Field","name":{"kind":"Name","value":"type"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"name"}}]}},{"kind":"Field","name":{"kind":"Name","value":"state"}},{"kind":"Field","name":{"kind":"Name","value":"thumbnail"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"loadData"},"value":{"kind":"BooleanValue","value":true}},{"kind":"Argument","name":{"kind":"Name","value":"pageNumber"},"value":{"kind":"IntValue","value":"1"}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"url"}}]}}]}}]} as unknown as DocumentNode<ThingItemFragment, unknown>;
export const ThingDetailFragmentDoc = {"kind":"Document","definitions":[{"kind":"FragmentDefinition","name":{"kind":"Name","value":"ThingDetail"},"typeCondition":{"kind":"NamedType","name":{"kind":"Name","value":"Thing"}},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"title"}},{"kind":"Field","name":{"kind":"Name","value":"type"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"name"}}]}},{"kind":"Field","name":{"kind":"Name","value":"source"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"connectorId"}},{"kind":"Field","name":{"kind":"Name","value":"connectorId"}}]}},{"kind":"Field","name":{"kind":"Name","value":"date"}},{"kind":"Field","name":{"kind":"Name","value":"classId"}},{"kind":"Field","name":{"kind":"Name","value":"state"}},{"kind":"Field","name":{"kind":"Name","value":"thumbnail"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"loadData"},"value":{"kind":"BooleanValue","value":true}},{"kind":"Argument","name":{"kind":"Name","value":"pageNumber"},"value":{"kind":"IntValue","value":"1"}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"url"}}]}}]}}]} as unknown as DocumentNode<ThingDetailFragment, unknown>;
export const InsertCorrespondentDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"insertCorrespondent"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"InsertCorrespondentInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"insertCorrespondent"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"correspondent"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}}]}}]}}]}}]} as unknown as DocumentNode<InsertCorrespondentMutation, InsertCorrespondentMutationVariables>;
export const GetThingByIdDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"getThingById"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"id"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"ID"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"thingById"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"id"},"value":{"kind":"Variable","name":{"kind":"Name","value":"id"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"FragmentSpread","name":{"kind":"Name","value":"ThingDetail"}}]}}]}},...ThingDetailFragmentDoc.definitions]} as unknown as DocumentNode<GetThingByIdQuery, GetThingByIdQueryVariables>;
export const ThingsSearchDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"thingsSearch"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"things"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"nodes"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"FragmentSpread","name":{"kind":"Name","value":"ThingItem"}}]}}]}}]}},...ThingItemFragmentDoc.definitions]} as unknown as DocumentNode<ThingsSearchQuery, ThingsSearchQueryVariables>;