import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import "./index.css";
import Auth from "./Auth";
import * as serviceWorker from "./serviceWorker";
import { setAuthorizationToken } from "./Actions/auth";
import LoginContainer from './Components/login/LoginContainer';
import './layout/layout.scss';

setAuthorizationToken(localStorage.getItem('Token'));

ReactDOM.render(
  <BrowserRouter>
  <Switch>
    <Route path='/login' component={LoginContainer} />
    <Auth />
  </Switch>
  </BrowserRouter>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
