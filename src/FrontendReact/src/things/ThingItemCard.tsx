import React from "react";
import graphql from 'babel-plugin-relay/macro'
import { ThingItemCard_Thing$key } from "./__generated__/ThingItemCard_Thing.graphql";
import { useFragment } from "react-relay";
import { Card, CardContent, Typography } from "@mui/material";

const ThingsItemCard: React.FC<{ data: ThingItemCard_Thing$key }> =
    ({ data }) => {

        const thingsItemFragment = graphql`
    fragment ThingItemCard_Thing on Thing
    {
        id
        title
        type {
          name
        }
        state
    }`;

        const thing = useFragment<ThingItemCard_Thing$key>(thingsItemFragment, data);
        return (
            <Card variant="outlined">
                <CardContent>
                    <Typography gutterBottom noWrap variant="h6" component="h6">
                        {thing.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Lizards are a widespread group of squamate reptiles, with over 6,000
                        species, ranging across all continents except Antarctica
                    </Typography>
                </CardContent>
            </Card>
        )
    }

export default ThingsItemCard;   