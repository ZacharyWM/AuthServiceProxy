import React, { useEffect, Fragment, useState } from 'react';
import { useHistory } from 'react-router';
import queryString from 'query-string'
import axios from 'axios'
import config from './../config/appConfigService'


//https://localhost:5001?redirect_uri=https://www.hcss.com/
export const Authorizing = (props) => {
    const history = useHistory()

    let yOffset = 40
    let xOffset = 40

    let movingClockwise = false

    let radius = 10



    let [coordinates, setCoordinates] = useState({x: radius, y: 0, quadrant: 1})

    const incrementSize = 1

   // const [angle, setAngle] = useState(90)




   let getY = (x) => Math.sqrt(Math.pow(radius, 2) - Math.pow(x, 2))

    let styling = {
        position: 'fixed', /* or absolute */
        top: `${yOffset}%`, 
        left: `${xOffset + radius}%` 
    }



    useEffect(() => {

        const interval = setInterval(() => {

            // TODO: When X is close to 0, the jump for Y is very large. 
            // Need to modulate the X increment to keep a constant angular velocity

            let nextX = coordinates.x
            let nextY = -(coordinates.y) // use the inverse since we are measuring from the top
            let quadrant = coordinates.quadrant
            
            switch (quadrant) {
                case 1:
                    nextX -= incrementSize
                    nextY = -Math.abs(getY(nextX))
                    quadrant = (nextX > 0) ? 1 : 2
                    break
                case 2:
                    nextX -= incrementSize
                    nextY = -(Math.abs(getY(nextX)))
                    quadrant = (Math.abs(nextX) < radius) ? 2 : 3
                    break
                case 3:
                    nextX += incrementSize
                    nextY = (Math.abs(getY(nextX)))
                    quadrant = (nextX < 0) ? 3 : 4    
                    break
                case 4:
                    nextX += incrementSize
                    nextY = Math.abs(getY(nextX))
                    quadrant = (nextX < radius) ? 4 : 1      
                    break              
                default:
                    break
            }

            setCoordinates(() => ({
                x: nextX, 
                y: nextY, 
                quadrant: quadrant
            }))



            /*
                                |
                        2 (-,+) |  1 (+,+)
                                |         
                         ----------------
                                |        
                        3 (-,-) |  4 (+,-)
                                |          
             */


             console.log(`x: ${coordinates.x}, y: ${coordinates.y}, quad: ${quadrant}`)


        }, 50);
        return () => clearInterval(interval);

    },[coordinates])

    styling.top = `${(coordinates.y) + yOffset}%`
    styling.left = `${coordinates.x + xOffset}%`


    // useEffect(() => {

    //     const { code, scope, state } = queryString.parse(props.location.search)

    //     const payload = {
    //         "Code": code,
    //         "Scope": scope,
    //         "State": props.location.pathname,
    //         "RedirectUri": config.redirectUri,
    //         "ClientRedirectUri": JSON.parse(state).clientUri
    //     }

    //     axios.post("/auth/google", payload)
    //          .then(response => {
    //              const client_redirect_uri = response.data.client_redirect_uri

    //              if (client_redirect_uri && client_redirect_uri.length > 0){
    //                 window.location = client_redirect_uri
    //              }
    //              else {
    //                 history.push({
    //                     pathname: '/dashboard',
    //                     state: { authCode: response.data.auth_code }
    //                 })
    //              }

    //          }).catch(error => {
    //             console.log(error)
    //          })
       
    // }, []) // should only run once


    return (
        <Fragment>
            <div className="primaryFont" >Authorizing...</div>
            <div className=" primaryFont" style={styling}>@</div>
        </Fragment>

    )

}