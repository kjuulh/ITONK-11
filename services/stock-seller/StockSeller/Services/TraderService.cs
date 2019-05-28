using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockSeller.Services
{
    public class TraderService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public TraderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<RequestViewModel> SellShare(Guid accountId, Guid portfolioId, Guid shareId, int amount)
        {
            var serviceDNS = Environment.GetEnvironmentVariable("TRADER_SERVICE_DNS");
            if (string.IsNullOrEmpty(serviceDNS))
                throw new NullReferenceException("TRADER_SERVICE_DNS url is null");
            var servicePORT = Environment.GetEnvironmentVariable("TRADER_SERVICE_PORT");
            if (string.IsNullOrEmpty(servicePORT))
                throw new NullReferenceException("TRADER_SERVICE_PORT url is null");

            var requestUri = "http://" + serviceDNS + ":" + servicePORT + $"/api/Trader/sell";
            var request = HttpRequestPost(requestUri,
                new
                {
                    AccountId = accountId,
                        PortfolioId = portfolioId,
                        ShareId = shareId,
                        Amount = amount
                }, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<RequestViewModel>();

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

        public class RequestViewModel
        {
            public Guid RequestId { get; set; }
            public Guid ShareId { get; set; }
            public Guid OwnerAccountId { get; set; }
            public Guid PortfolioId { get; set; }
            public int Amount { get; set; }
            public string Status { get; set; }
            public DateTime DateAdded { get; set; }
            public DateTime DateClosed { get; set; }
        }
    }
}