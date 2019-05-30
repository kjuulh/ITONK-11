using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Authentication.Services
{
    public interface IBankService
    {
        Task<bool> CreateBankAndAccount(Guid userId);
    }

    public class BankService : IBankService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BankService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateBankAndAccount(Guid userId)
        {
            var serviceDNS = Environment.GetEnvironmentVariable("BANK_SERVICE_DNS");
            if (string.IsNullOrEmpty(serviceDNS)) throw new NullReferenceException("BANK_SERVICE_DNS url is null");
            var servicePORT = Environment.GetEnvironmentVariable("BANK_SERVICE_PORT");
            if (string.IsNullOrEmpty(servicePORT)) throw new NullReferenceException("BANK_SERVICE_PORT url is null");
            var requestUri = "http://" + serviceDNS + ":" + servicePORT + "/api/Bank";
            var request = HttpRequestPost(
                requestUri, new { UserId = userId },
                out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        private HttpRequestMessage HttpRequestPost(string requestUri, object content, out HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                requestUri);
            request.Headers.Add("Accept", "application/json");

            var jsonContent = JsonConvert.SerializeObject(content, Formatting.None).ToLower();

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            client = _httpClientFactory.CreateClient();
            return request;
        }
    }
}