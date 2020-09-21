import config from './../config/appConfigService'

//https://auth0.com/docs/protocols/oauth2/mitigate-csrf-attacks
const state = "I don't know what I want here right now"

const googleScopes = [
    'openid', 
    'email',
    'https://www.googleapis.com/auth/contacts.readonly',
    "https://www.googleapis.com/auth/userinfo.profile"
]

const authParamters = [
    `redirect_uri=${encodeURI(config.redirectUri)}`,
    "response_type=code",
    `client_id=${config.googleAuthClientId}`,
    `scope=${encodeURI(googleScopes.join(' '))}`,
    "access_type=offline",
    "approval_prompt=force",
    `state=${state}`
]

export const googleEndpoints = {
    auth: `https://accounts.google.com/o/oauth2/auth?${authParamters.join('&')}`
}





