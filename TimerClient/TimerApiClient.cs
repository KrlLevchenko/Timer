using System.Net.Http;
using System.Threading.Tasks;

namespace TimerClient
{
    public class TimerApiClient
    {
        private readonly HttpClient _httpClient;

        public TimerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task CreateFile(string name, string content)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/drive/" + name)
            {
                Content = new StringContent(content)
            };
            var resp = await _httpClient.SendAsync(requestMessage);
            resp.EnsureSuccessStatusCode();
        }
    }
}