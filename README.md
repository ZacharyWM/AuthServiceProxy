# AuthServiceProxy

Purpose: This app is a proxy service that uses Google's OAuth2.0/OIDC service to authenticate and authorize users.

Notes:
1. It does not use any 3rd party authorization flow packages. I wrote my own implementation.
2. This app stores the Google access token in Azure CosmosDB (using MongoDB), and does not expose the Google access token to the client.
3. Any client can authenticate/authorize using the "Authorization Code" flow.

Potential future development ideas:
1. Use other auth providers (FaceBook, Spotify, Twitter, Microsoft, etc.), so that clients can access APIs from all these sources through this app.
2. Write API requests to the auth service providers.
3. Chron jobs that use the auth provider services on the clients behalf (i.e. scheduled emails or posts). 
