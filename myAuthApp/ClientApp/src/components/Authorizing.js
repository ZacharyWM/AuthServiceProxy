import React, { useState, useEffect, Fragment } from 'react';
import { Route, useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'

export const Authorizing = (props) => {
    const history = useHistory()


    useEffect(() => {

        const { code, scope } = queryString.parse(props.location.search)
       // console.log(queryString.parse(props.location.search))

        // let state = { "state": props.location }
        // let redirect_uri = { "redirect_uri": "https://localhost:5001/dashboard" }

        const payload = {
            "code": code,
            "scope": scope,
            "state": props.location.pathname,
            "redirect_uri": config.redirectUri
        }

        axios.post("/login/google", payload)
        .then(response => {
            console.log(response)

            history.push({
                pathname: '/dashboard',
                state: { user: response.data }
              })
        }).catch(error => {
            console.log(error)
        })




        
    }, []) // should only run once


    return (
        <div>Authorizing...</div>
    )

}