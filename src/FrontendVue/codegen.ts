import type { CodegenConfig } from "@graphql-codegen/cli";

const config: CodegenConfig = {
    schema: "http://localhost:5000/graphql/",
    documents: ["src/**/*.vue", "src/**/*.graphql", "!src/gql/**/*"],
    ignoreNoDocuments: true, // for better experience with the watcher
    generates: {
        "./src/generated/": {
            preset: "client",
            config: {
                useTypeImports: true,
            },
            plugins: [],
        },
        "schema.graphql": {
            plugins: ["schema-ast"],
        },
    },
};

export default config;
