namespace myAuthApp.Models
{
    public class AuthCode
    {
        public AuthCode() { }
        public AuthCode(string code, string scope, string state, string redirect_uri)
        {
            this.code = code;
            this.scope = scope;
            this.state = state;
            this.redirect_uri = redirect_uri;
        }

        public string code { get; set; }
        public string scope { get; set; }
        public string state { get; set; }
        public string redirect_uri { get; set; }
    }
}