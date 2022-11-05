import React from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App";
import { RelayEnvironmentProvider } from "react-relay";
import RelayEnvironment from "./RelayEnvironment";



const container = document.getElementById("root")!;
const root = createRoot(container);
root.render(

  <React.StrictMode>
    <RelayEnvironmentProvider environment={RelayEnvironment}>
      <App />
    </RelayEnvironmentProvider>
  </React.StrictMode>
);

