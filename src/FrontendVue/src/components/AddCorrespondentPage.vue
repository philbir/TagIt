<template>
    <v-form>
        <v-container>
            <v-row>
                <v-col cols="12" md="6">
                    <v-text-field v-model="name" label="Name" required></v-text-field>
                </v-col>
            </v-row>
            <v-btn color="success" class="mr-4" @click="onSave"> Save </v-btn>
        </v-container>
    </v-form>
</template>

<script setup lang="ts">
import { useMutation } from "@urql/vue";
import { ref } from "vue";
import { graphql } from "../generated";

const insertMutation = graphql(/* GraphQL */ `
    mutation insertCorrespondent($input: InsertCorrespondentInput!) {
        insertCorrespondent(input: $input) {
            correspondent {
                id
                name
            }
        }
    }
`);

const mutation = useMutation(insertMutation);
const name = ref("");

const onSave = async () => {
    var result = await mutation.executeMutation({
        input: { name: name.value },
    });

    name.value = "";
    console.log(result);
};
</script>
