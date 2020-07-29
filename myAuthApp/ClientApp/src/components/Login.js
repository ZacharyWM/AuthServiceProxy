import React, { useState } from 'react';
import {googleEndpoints} from '../auth/Auth_Google'
import GoogleButton from 'react-google-button'


export const Login = (props) => {

  const [showLogin, setShowLogin] = useState(true);
  const [loginInfo, setLoginInfo] = useState({userName:'', password:''})
  const [signUpInfo, setSignUpInfo] = useState({userName:'', password:'', email:''})

  return (
    <div className="login-page">
      <div className="form">
      <GoogleButton onClick={() => window.location.href = googleEndpoints.auth} />

        {/* { showLogin &&  
          <LoginForm  
            loginInfo={loginInfo} 
            setLoginInfo={setLoginInfo}
            setShowLogin={setShowLogin }
            />
        }
        { !showLogin && 
          <SignUpForm
            signUpInfo={signUpInfo} 
            setSignUpInfo={setSignUpInfo}
            setShowLogin={setShowLogin }
            />       
        }        */}
      </div>
    </div>
  )
}

const LoginForm = (props) => {
  return(
    <div className="login-form">

      <GoogleButton onClick={() => window.location.href = googleEndpoints.auth} />

      <input type="text" placeholder="username"/>
      <input type="password" placeholder="password"/>
          <button>login</button>
      <p className="message">Not registered? <a onClick={() => props.setShowLogin(false)}>Create an account</a></p>
    </div>
  )
}

const SignUpForm = (props) => {
  return (
    <div className="register-form">
      <input type="text" placeholder="name"/>
      <input type="password" placeholder="password"/>
          <input type="text" placeholder="email address" />
          <button>create</button>
      <p className="message">Already registered? <a onClick={() => props.setShowLogin(true)}>Sign In</a></p>
    </div>
  )
}