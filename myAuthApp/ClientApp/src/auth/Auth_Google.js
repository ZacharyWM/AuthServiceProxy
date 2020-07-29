const client_id = "884429750806-4lj7ea238v67c5681d707r3napu02q1e.apps.googleusercontent.com"
const redirect_uri_encoded = "https%3A%2F%2Flocalhost%3A5001%2Fauthorizing"

// How to use the state param
//https://auth0.com/docs/protocols/oauth2/mitigate-csrf-attacks
const state = "I don't know what I want here right now"

const googleScopes = [
    'openid', 
    'email',
    'https://www.googleapis.com/auth/contacts.readonly'
]

const authParamters = [
    `redirect_uri=${redirect_uri_encoded}`,
    "response_type=code",
    `client_id=${client_id}`,
    `scope=${encodeURI(googleScopes.join(' '))}`,
    "access_type=offline",
    "approval_prompt=force",
    `state=${state}`
]

export const googleEndpoints = {
    auth: `https://accounts.google.com/o/oauth2/auth?${authParamters.join('&')}`
}





