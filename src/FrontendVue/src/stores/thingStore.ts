import { graphqlClient } from './../graphqlClient';
import { defineStore } from 'pinia'

export const useLookupStore = defineStore('things', () => {

    const getThingById = async () => {

        return null;
    }

    return { getThingById }
})
