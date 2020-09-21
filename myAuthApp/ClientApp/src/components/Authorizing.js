import React, { useEffect } from 'react';
import { useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'

export const Authorizing = (props) => {
    const history = useHistory()

    useEffect(() => {

        const { code, scope } = queryString.parse(props.location.search)

        const payload = {
            "code": code,
            "scope": scope,
            "state": props.location.pathname,
            "redirect_uri": config.redirectUri
        }

        axios.post("/auth/google", payload)
             .then(response => {
                 console.log(response)

                 const redirect_uri = response.data.redirect_uri

                 if (redirect_uri && redirect_uri.length > 0){
                    window.location = redirect_uri + "?auth_code=knwunwcao3nd"
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