namespace myAuthApp.Models {
    public class IdentityProviderAuthCodeDetails {
        public IdentityProviderAuthCodeDetails() { }
        public IdentityProviderAuthCodeDetails(string code, string scope, string state, string redirect_uri, string client_redirect_uri) {
            this.Code = code;
            this.Scope = scope;
            this.State = state;
            this.RedirectUri = redirect_uri;
            this.ClientRedirectUri = client_redirect_uri;
        }

        public string Code { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string RedirectUri { get; set; }
        public string ClientRedirectUri { get; set; }
    }
}