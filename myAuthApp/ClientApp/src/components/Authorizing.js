import React, { useEffect } from 'react';
import { useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'

export const Authorizing = (props) => {
    const history = useHistory()

    useEffect(() => {

        const { code, scope, state } = queryString.parse(props.location.search)

        console.log(state)

        const payload = {
            "Code": code,
            "Scope": scope,
            "State": props.location.pathname,//JSON.parse(state).clientUri
            "RedirectUri": config.redirectUri,
            "ClientRedirectUri": JSON.parse(state).clientUri
        }

        console.log({payload})

        axios.post("/auth/google", payload)
             .then(response => {
                 console.log(response)
                 console.log(response.data.client_redirect_uri)

                 const client_redirect_uri = response.data.client_redirect_uri

                 // DEFECT: 
                 // Why isn't it going to dashboard when there is no client_redirect_uri?
                 // Am I even getting to this point?

                 if (client_redirect_uri && client_redirect_uri.length > 0){
                    window.location = client_redirect_uri
                 }
                 else {
                    history.push({
                        pathname: '/dashboard',
                        state: { user: response.data }
                    })
                 }

             }).catch(error => {
                console.log(error)
             })
       
    }, []) // should only run once


    return (
        <div>Authorizing...</div>
    )

}