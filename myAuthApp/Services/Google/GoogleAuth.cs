using System.Net.Http;
using System.Threading.Tasks;

namespace myAuthApp.Services
{
    public class GoogleAuth : IGoogleAuth
    {
        IHttpClientFactory _clientFactory;

        public GoogleAuth(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }



        //private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> GetToken(string authCode)
        {

            //https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
            // uses HttpClient client = new HttpClient();
            //var res = _httpClient.GetAsync("");



            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            return "";
        }
    }
}