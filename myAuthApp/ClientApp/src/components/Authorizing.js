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
                 const client_redirect_uri = response.data.client_redirect_uri

                 if (client_redirect_uri && client_redirect_uri.length > 0){
                    window.location = client_redirect_uri
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
        <Fragment>
            <div className="primaryFont" >Authorizing...</div>
            <AroundTheWorld/>
        </Fragment>

    )

}