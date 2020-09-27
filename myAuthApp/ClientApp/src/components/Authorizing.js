import React, { useEffect, Fragment, useState } from 'react';
import { useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'

export const Authorizing = (props) => {
    const history = useHistory()

    let size = 20

    let yMin = 30
    let yMax = yMin + size
    let yMidpoint = (yMin + yMax) / 2

    let xMin = 40
    let xMax = xMin + size
    let xMidpoint = (xMin + xMax) / 2


    // let [x, setX] = useState(xMin)
    // let [y, setY] = useState(yMin)
    let [coordinates, setCoordinates] = useState({x: xMin, y: yMin})
    const changeSize = 0.05

   // const [angle, setAngle] = useState(90)

    let styling = {
        position: 'fixed', /* or absolute */
        top: '30%', // min 30, max 50
        left: '40%' // min 40, max 60
    }

    console.log(`x: ${coordinates.x}, y: ${coordinates.y}`)


    useEffect(() => {

        const interval = setInterval(() => {

            let quadrant_1 = coordinates.x <= xMidpoint && coordinates.y >= yMidpoint
            let quadrant_2 = coordinates.x >= xMidpoint && coordinates.y >= yMidpoint
            let quadrant_3 = coordinates.x >= xMidpoint && coordinates.y <= yMidpoint
            let quadrant_4 = coordinates.x <= xMidpoint && coordinates.y <= yMidpoint

            if(quadrant_1){
                setCoordinates(coordinates => ({x: (coordinates.x + changeSize), y: (coordinates.y + changeSize)}))
            }
            else if(quadrant_2){
                setCoordinates(coordinates => ({x: (coordinates.x + changeSize), y: (coordinates.y - changeSize)}))
            }
            else if(quadrant_3){
                setCoordinates(coordinates => ({x: (coordinates.x - changeSize), y: (coordinates.y - changeSize)}))
            }
            else if (quadrant_4){
                setCoordinates(coordinates => ({x: (coordinates.x - changeSize), y: (coordinates.y + changeSize)}))
            }

        }, 5);
        return () => clearInterval(interval);

    },[coordinates])

    styling.top = `${coordinates.y}%`
    styling.left = `${coordinates.x}%`


    useEffect(() => {

        // const { code, scope, state } = queryString.parse(props.location.search)

        // const payload = {
        //     "Code": code,
        //     "Scope": scope,
        //     "State": props.location.pathname,
        //     "RedirectUri": config.redirectUri,
        //     "ClientRedirectUri": JSON.parse(state).clientUri
        // }

        // axios.post("/auth/google", payload)
        //      .then(response => {
        //          const client_redirect_uri = response.data.client_redirect_uri

        //          if (client_redirect_uri && client_redirect_uri.length > 0){
        //             window.location = client_redirect_uri
        //          }
        //          else {
        //             history.push({
        //                 pathname: '/dashboard',
        //                 state: { authCode: response.data.auth_code }
        //             })
        //          }

        //      }).catch(error => {
        //         console.log(error)
        //      })
       
    }, []) // should only run once


    return (
        <Fragment>
            <div className="primaryFont">Authorizing...</div>
            <div className=" primaryFont" style={styling}>@</div>
        </Fragment>

    )

}