using System;
using System.Net.Http;
using System.Threading.Tasks;
using static StockBuyer.Services.PortfolioService;

namespace StockBuyer.Services
{
    public interface IPortfolioService
    {
        Task<PortfolioViewModel> GetPortfolioByUser(Guid userId);
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PortfolioService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PortfolioViewModel> GetPortfolioByUser(Guid userId)
        {
            var shareControlServiceDNS = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_DNS");
            if (string.IsNullOrEmpty(shareControlServiceDNS))
                throw new NullReferenceException("SHARECONTROL_SERVICE_DNS url is null");
            var shareControlServicePORT = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_PORT");
            if (string.IsNullOrEmpty(shareControlServicePORT))
                throw new NullReferenceException("SHARECONTROL_SERVICE_PORT url is null");

            var requestUri = "http://" + shareControlServiceDNS + ":" + shareControlServicePORT + $"/api/Portfolio/user/{userId}";

            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PortfolioViewModel>();
            return null;
        }

        private HttpRequestMessage HttpRequestGet(string requestUri, out HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                requestUri);
            request.Headers.Add("Accept", "application/json");

            client = _httpClientFactory.CreateClient();
            return request;
        }

        public class PortfolioViewModel
        {
            public Guid PortfolioId { get; set; }
        }
    }
}