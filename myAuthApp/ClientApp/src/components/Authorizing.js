import React, { useEffect, Fragment, useState } from 'react';
import { useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'

export const Authorizing = (props) => {
    const history = useHistory()

    let size = 20
    let radius = 10

    let yMin = 40
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

   let getNextX = (currentX, change) => {
        let nextX = currentX + change
        if(nextX > xMax) {
            nextX = xMax
        }
        if(nextX < xMin){
            nextX = xMin
        }
        return nextX
   } 

   let solveForY = (x) => Math.sqrt(Math.pow(radius, 2) - Math.pow((x - 50),2)) + 50

    let styling = {
        position: 'fixed', /* or absolute */
        top: '30%', // min 30, max 50
        left: '40%' // min 40, max 60
    }

   // console.log(`x: ${coordinates.x}, y: ${coordinates.y}`)


    useEffect(() => {

        const interval = setInterval(() => {

            let quadrant_1 = coordinates.x <= xMidpoint && coordinates.y >= yMidpoint
            let quadrant_2 = coordinates.x > xMidpoint && coordinates.y > yMidpoint
            let quadrant_3 = coordinates.x >= xMidpoint && coordinates.y <= yMidpoint
            let quadrant_4 = coordinates.x < xMidpoint && coordinates.y <= yMidpoint



            if(quadrant_1){
                console.log('quad 1')
                let nextX = getNextX(coordinates.x, changeSize)
                let nextY = solveForY(nextX)
                setCoordinates(coordinates => ({x: nextX, y: nextY}))
            }
            else if(quadrant_2){
                console.log('quad 2')
                let nextX = getNextX(coordinates.x, changeSize)
                let nextY = solveForY(nextX)
                setCoordinates(coordinates => ({x: nextX, y: nextY}))
            }
            else if(quadrant_3){
                console.log('quad 3')

                let nextX = getNextX(coordinates.x, -changeSize)
                let nextY = solveForY(nextX)
                nextY = nextY - (nextY - yMidpoint)
                setCoordinates(coordinates => ({x: nextX, y: nextY}))
            }
            else if (quadrant_4){
                console.log('quad 4')
                let nextX = getNextX(coordinates.x, changeSize)
                let nextY = solveForY(nextX)
                setCoordinates(coordinates => ({x: nextX, y: nextY}))
            }

        }, 10);
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