
import { NonIndexRouteObject } from "react-router-dom";


export interface RouteDefinition extends NonIndexRouteObject {
  name: string;
  path: string;
  link: string;
}

export const navigation: RouteDefinition[] = [

];
