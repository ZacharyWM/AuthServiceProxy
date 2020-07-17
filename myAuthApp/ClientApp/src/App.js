import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Dashboard } from './components/Dashboard';
import { Authorizing } from './components/Authorizing'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;


  render () {

    return (
       <Layout>
        <Route exact path='/' component={Login} />
        <Route exact path='/dashboard' component={Dashboard} />
        <Route exact path='/authorizing' component={Authorizing} />
       </Layout>
    );
  }
}
