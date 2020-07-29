import React, { useEffect } from 'react';
import { Route, useHistory } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Dashboard } from './components/Dashboard';
import { Authorizing } from './components/Authorizing'

import './custom.css'


const App = (props) => {
  const history = useHistory()

  useEffect(() => {
    if(window.location.pathname == "/index.html"){    
      // TODO: figure out why it tries to go to /index.html on startup   
      history.push('/')
    }
  },[window.location.pathname])

 
  return (
     <Layout>
      <Route exact path='/' component={Login} />
      <Route exact path='/dashboard' component={Dashboard} />
      <Route exact path='/authorizing' component={Authorizing} />
     </Layout>
  );
}

export default App


