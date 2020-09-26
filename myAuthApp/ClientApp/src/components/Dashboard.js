import React, { useState, useEffect } from 'react';
import queryString from 'query-string'
import axios from 'axios'


export const Dashboard = (props) => {

        const [user, setUser] = useState({})

        console.log("auth code")
        console.log(props.location.state.authCode)

        useEffect(() => {

        axios.post("/token/exchange", {Code: props.location.state.authCode})
             .then(response => {

                console.log(response)

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

    if (user.firstName && user.firstName.length > 0){
        return (
            <div className="primaryFont">You made it to the dashboard, {user.firstName}!</div>
        )
    }

    return (
        <div className="primaryFont">Dashboard</div>
    )

}