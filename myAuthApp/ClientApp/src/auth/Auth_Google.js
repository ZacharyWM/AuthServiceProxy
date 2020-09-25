import config from './../config/appConfigService'

//https://auth0.com/docs/protocols/oauth2/mitigate-csrf-attacks

const googleScopes = [
   'openid', 
    'email',
  // 'https://www.googleapis.com/auth/contacts.readonly',
     "https://www.googleapis.com/auth/userinfo.profile"
]

// const authParamters = [
//     `redirect_uri=${encodeURI(config.redirectUri)}`,
//     "response_type=code",
//     `client_id=${config.googleAuthClientId}`,
//     `scope=${encodeURI(googleScopes.join(' '))}`,
//     "access_type=offline",
//     "approval_prompt=force",
//     `state=${state}`
// ]

let getAuthParameters = (clientUriForRedirect) => {
    return [
        `redirect_uri=${encodeURI(config.redirectUri)}`,
        "response_type=code",
        `client_id=${config.googleAuthClientId}`,
        `scope=${encodeURI(googleScopes.join(' '))}`,
        "access_type=offline",
        "approval_prompt=force",
        `state=${JSON.stringify({ clientUri : clientUriForRedirect })}`
    ].join('&')
}

export const googleEndpoints = {
    getAuthEndpoint: (clientUri) => `https://accounts.google.com/o/oauth2/auth?${getAuthParameters(clientUri)}`
}





