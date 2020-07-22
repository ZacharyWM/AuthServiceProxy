import React, { useState, useEffect, Fragment } from 'react';
import queryString from 'query-string'
import axios from 'axios'

export const Authorizing = (props) => {


    useEffect(() => {

        const { code, scope } = queryString.parse(props.location.search)
       // console.log(queryString.parse(props.location.search))

        // let state = { "state": props.location }
        // let redirect_uri = { "redirect_uri": "https://localhost:5001/dashboard" }

        const payload = {
            "code": code,
            "scope": scope,
            "state": props.location.pathname,
            "redirect_uri": "https://localhost:5001/authorizing"
        }

        console.log(payload)

        axios.post("/login/google", payload).then(response => console.log(response))



        // send to server, get auth token, save to DB

    }, []) // should only run once


    return (
        <div>Authorizing...</div>
    )

}