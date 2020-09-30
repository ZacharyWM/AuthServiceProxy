import React, { useEffect, Fragment, useState } from 'react';


export const AroundTheWorld = (props) => {

    let yOffset = 50 // % distance from the top
    let xOffset = 50  // % distance from the left

    let radius = 15
    let radianIncrement = 0.0025
    let interval_ms = 1

    let [coordinates, setCoordinates] = useState({
        x: radius, 
        y: 0, 
        radian: 0.0,
        rocketAngle: 270
    })

    let rocketStyling = {
        position: 'fixed', /* or absolute */
        top: `${yOffset}%`, 
        left: `${xOffset + radius}%`, 
        transform: 'rotate(270deg)'
    }

    let earthStyling = {
        position: 'fixed', /* or absolute */
        top: `${yOffset}%`, 
        left: `${xOffset}%`
    }

    useEffect(() => {

        const interval = setInterval(() => {

            let nextRadian = coordinates.radian += radianIncrement
            if(nextRadian >= (2 * Math.PI)){
                nextRadian = 0.0
            }
            let nextX = (radius * Math.cos(nextRadian))
            let nextY = (radius * Math.sin(nextRadian))
            
            let nextRocketAngle = nextRadian * (180/Math.PI) + 90

            setCoordinates(() => ({
                x: nextX, 
                y: nextY, 
                radian: nextRadian,
                rocketAngle: nextRocketAngle
            }))
 
            
        }, interval_ms);
        return () => clearInterval(interval);

    },[coordinates])

    // widthToHeightRatio is used to keep a constant distance between the earth and the rock (i.e. a perfectly circlular path)
    let widthToHeightRatio = window.screen.width/window.screen.height
    let yLocation = coordinates.y + yOffset
    let xLocation = (coordinates.x / widthToHeightRatio) + xOffset

    rocketStyling.top = `${yLocation}%`
    rocketStyling.left = `${xLocation}%`
    rocketStyling.transform = `rotate(${coordinates.rocketAngle}deg)`

    return ( 
        <Fragment>
            <div className=" primaryFont" style={rocketStyling}>

                {'}-->'}

            </div>
            <div className=" primaryFont" style={earthStyling}>earth</div>
        </Fragment>
    )

}

export default AroundTheWorld