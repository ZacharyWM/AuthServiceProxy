import React, { useState } from 'react';
import {googleEndpoints} from '../auth/Auth_Google'
import queryString from 'query-string'
import GoogleButton from 'react-google-button'


export const Login = (props) => {

  const { redirect_uri } = queryString.parse(props.location.search)

  let clientRedirectUri = redirect_uri

  return (
    <div className="login-page">
      <div id="welcomeToTheLoginPageMessage">Welcome to Zach's Auth Center</div>
      <div className="form">
        <GoogleButton type="dark" onClick={() => window.location.href = googleEndpoints.getAuthEndpoint(clientRedirectUri)} />
      </div>
    </div>
  )
}