using Functions.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Services
{
    public static class SignService
    {
        private const string Uri = "https://plainconcepts.woffu.com/api/signs";

        public static async Task Sign(string authToken, int userId)
        {
            var httpClient = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Post, Uri);
            var body = JsonConvert.SerializeObject(new Message(userId));

            message.Content = new StringContent(body, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            await httpClient.SendAsync(message);
        }
    }
}
