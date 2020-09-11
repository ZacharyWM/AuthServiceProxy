import devAppConfig from './appConfig_dev.json'
import prodAppConfig from './appConfig.json'

let envConfig = process.env == "development" ? devAppConfig : prodAppConfig

const config = {
    redirectUri: envConfig["redirect_uri"]
}

export default config