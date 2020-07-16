import React, { useState,useEffect, Fragment } from 'react';
import queryString from 'query-string'

export const Dashboard = (props) => {


    useEffect(() => {

        const {code, scope} = queryString.parse(props.location.search)
        console.log({code, scope})
        // send to server, get auth token, save to DB

    },[]) // should only run once
    

    return (
        <div>You made it to the dashboard!</div>
    )

}