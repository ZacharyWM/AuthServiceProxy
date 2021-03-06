import devAppConfig from './appConfig_dev.json'
import prodAppConfig from './appConfig.json'

let envConfig = process.env == "development" ? devAppConfig : prodAppConfig

const config = {
    redirectUri: window.location.origin + envConfig["redirect_uri"],
    googleAuthClientId: envConfig["google_auth_client_id"]
}

export default config