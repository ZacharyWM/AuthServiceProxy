import React, { useEffect, Fragment, useState } from 'react';
import { useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'
import AroundTheWorld from './AroundTheWorld'


export const Authorizing = (props) => {
    const history = useHistory()

    useEffect(() => {

        const { code, scope, state } = queryString.parse(props.location.search)

        

        const payload = {
            "Code": code,
            "Scope": scope,
            "State": props.location.pathname,
            "RedirectUri": config.redirectUri,
            "ClientRedirectUri": JSON.parse(state ?? '{}').clientUri
        }
        
        axios.post("/auth/google", payload)
             .then(response => {
                 const clientRedirectUri = response.data.client_redirect_uri

                 if (clientRedirectUri && clientRedirectUri.length > 0){
                    window.location = clientRedirectUri
                 }
                 else {
                    history.push({
                        pathname: '/dashboard',
                        state: { authCode: response.data.auth_code }
                    })
                 }

             }).catch(error => {
                console.log(error)
             })
       
    }, []) // should only run once


    return (
            <AroundTheWorld>
                {"Authorizing..."}
            </AroundTheWorld>

    )

}