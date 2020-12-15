import React, { useState, useEffect } from 'react';
import queryString from 'query-string'
import axios from 'axios'
import AroundTheWorld from './AroundTheWorld'


export const Dashboard = (props) => {

    const [user, setUser] = useState({})
    
    useEffect(() => {

    axios.post("/token/exchange", {Code: props.location.state.authCode})
        .then(response => {
            setUser({
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                email: response.data.email,
                roles: response.data.roles
            });
        }).catch(error => {
            console.log(error)
        })  
    }, [])



    return (
        <AroundTheWorld>
            {(user.firstName && user.firstName.length > 0) ?
                        <div className="primaryFont">You made it to the dashboard, {user.firstName}!</div> :
                        <div className="primaryFont">Dashboard</div>
            }
        </AroundTheWorld>
    )

}