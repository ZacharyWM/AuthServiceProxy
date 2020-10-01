import React, { useEffect, Fragment, useState } from 'react';
import SvgRocket from './icons/SvgRocket';
import SvgEarth from './icons/SvgEarth';


const AroundTheWorld = (props) => {

    let yOffset = 50 // % distance from the top
    let xOffset = 50  // % distance from the left

    let radius = 15
    let radianIncrement = 0.003
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
        top: `${yOffset - 6}%`, 
        left: `${xOffset - 3}%`
    }

    let iconAttributeStyling = {
        position: 'fixed', /* or absolute */
        top: `95%`, 
        left: `2%`,
        fontSize: '10px'
    }

    useEffect(() => {

        const interval = setInterval(() => {

            let nextRadian = coordinates.radian += radianIncrement
            if(nextRadian >= (2 * Math.PI)){
                nextRadian = 0.0
            }
            let nextX = (radius * Math.cos(nextRadian))
            let nextY = (radius * Math.sin(nextRadian))
            
            let nextRocketAngle = nextRadian * (180/Math.PI) + 135

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
                <SvgRocket/>
            </div>
            <div className="primaryFont" style={earthStyling}>
                <SvgEarth/>
            </div>
            <div className="primaryFont" style={iconAttributeStyling}>Icons made by <a href="https://www.flaticon.com/authors/dinosoftlabs" title="DinosoftLabs">DinosoftLabs</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>
        </Fragment>
    )

}

export default AroundTheWorld