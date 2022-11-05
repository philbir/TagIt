import React from "react";
import "./App.css";
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ThingsList from "./things/ThingsList";
import Dashboard from "./dashboard/Dashboard";

const App: React.FC = ({
}) => {
  return (
    <>
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position="static">
          <Toolbar>
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2 }}
            >
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              Tag It
            </Typography>
            <Button color="inherit">User</Button>
          </Toolbar>
        </AppBar>
      </Box>
      <Dashboard></Dashboard>
    </>
  );
};

export default App;
