import React, { useEffect, useContext } from 'react';
import { Route, useHistory } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Dashboard } from './components/Dashboard';
import { Authorizing } from './components/Authorizing'

import './custom.css'

const defaultAuthContextValue = {
  isAuthorized: false,
  isAuthorizing: false
}
// using Context in functional components
//https://www.taniarascia.com/using-context-api-in-react/
const AuthContext = React.createContext(defaultAuthContextValue)


const App = (props) => {
  const history = useHistory()

  useEffect(() => {
    if(window.location.pathname == "/index.html"){    
      // TODO: figure out why it tries to go to /index.html on startup   
      history.push('/')
    }
  },[window.location.pathname])

 
  return (
    <AuthContext.Provider value={defaultAuthContextValue}>
     <Layout>
      <Route exact path='/' component={Login} />
      <Route exact path='/dashboard' component={Dashboard} />
      <Route exact path='/authorizing' component={Authorizing} />
     </Layout>
    </AuthContext.Provider>

  );
}

export default App


