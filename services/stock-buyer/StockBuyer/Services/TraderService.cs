using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockBuyer.Services
{
    public interface ITraderService
    {
        Task<TraderService.ResponseViewModel> BuyShare(Guid requestId, Guid accountId, Guid portfolioId);
    }

    public class TraderService : ITraderService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public TraderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseViewModel> BuyShare(Guid requestId, Guid accountId, Guid portfolioId)
        {
            var serviceDNS = Environment.GetEnvironmentVariable("TRADER_SERVICE_DNS");
            if (string.IsNullOrEmpty(serviceDNS))
                throw new NullReferenceException("TRADER_SERVICE_DNS url is null");
            var servicePORT = Environment.GetEnvironmentVariable("TRADER_SERVICE_PORT");
            if (string.IsNullOrEmpty(servicePORT))
                throw new NullReferenceException("TRADER_SERVICE_PORT url is null");

            var requestUri = "http://" + serviceDNS + ":" + servicePORT + $"/api/Trader/buy/{requestId}";
            var request = HttpRequestPost(requestUri,
                new
                {
                    AccountId = accountId,
                        PortfolioId = portfolioId,

                }, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ResponseViewModel>();

            return null;
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

        public class ResponseViewModel
        {
            public string Status { get; set; }
        }
    }
}