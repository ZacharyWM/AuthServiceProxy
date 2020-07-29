import React, { useState, useEffect } from 'react';

export const Dashboard = (props) => {

    const [user, setUser] = useState(props.location.state.user)


    return (
        <div>You made it to the dashboard, user.firstName!</div>
    )

}