# AuthServiceProxy

This app is a proxy service that uses Google's OAuth2.0/OIDC service to authenticate and authorize users.

Potential future development ideas:
1. Use other auth providers (FaceBook, Spotify, Twitter, Microsoft, etc.), so that clients can access APIs from all these sources through this app.
2. Write API requests to the auth service providers.
3. Chron jobs that use the auth provider services on the clients behalf (i.e. scheduled emails or posts). 
