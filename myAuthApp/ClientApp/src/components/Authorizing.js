import React, { useState,useEffect, Fragment } from 'react';
import queryString from 'query-string'
import axios from 'axios'

export const Authorizing = (props) => {


    useEffect(() => {

        const {code, scope} = queryString.parse(props.location.search)
        console.log(queryString.parse(props.location.search))

        axios.post("/login/auth", {code, scope}).then(response => console.log(response))
        
        

        // send to server, get auth token, save to DB

    },[]) // should only run once
    

    return (
        <div>Authorizing...</div>
    )

}