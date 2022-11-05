import React from "react";
import graphql from 'babel-plugin-relay/macro'
import { useLazyLoadQuery } from "react-relay";
import { ThingsListQuery } from "./__generated__/ThingsListQuery.graphql";
import ThingsItemCard from "./ThingItemCard";
import { Grid } from "@mui/material";

const thingsQuery = graphql`
query ThingsListQuery {
  things {
    nodes {
      id
      ... ThingItemCard_Thing
    }
  }
}
`;

const ThingsList: React.FC = () => {
  const data = useLazyLoadQuery<ThingsListQuery>(thingsQuery, {});
  return (<Grid container spacing={3}>
    {data.things?.nodes?.map(item => (
      <Grid key={item.id} item xs={12} sm={6} md={3}>
        <ThingsItemCard key={item.id} data={item}></ThingsItemCard>
      </Grid>
    ))}
  </Grid>
  );
}

export default ThingsList;