using GPT.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace GPT.Services
{
    public class GPTSample : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public GPTSample(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));

            _apiKey = Environment.GetEnvironmentVariable("GPT_API_KEY");
        }

        public async Task RunAsync()
        {
            await GetResourceAsync();
        }

        public async Task GetResourceAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var request = new GPTRequest
            {
                Messages = new GPTRequest.Message[]
                {
                    new GPTRequest.Message { Role = "system", Content = "You are a helpfull freight forwarding agent"},
                    new GPTRequest.Message { Role = "user", Content = "What is the size of a 20' container?"}
                }
            };

           string requestJson = JsonConvert.SerializeObject(request);

           HttpContent httpContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

           var response = await httpClient.PostAsync("", httpContent);

           if (!response.IsSuccessStatusCode)
           {
                string error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {error}");
           }
        }
    }
}
