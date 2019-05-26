using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static StockProvider.Services.PortfolioService;

namespace StockProvider.Services
{
    public interface IPortfolioService
    {
        Task<PortfolioViewModel> CreatePortfolio(Guid userID);
        Task<PortfolioViewModel> GetPortfolioByUser(Guid userId);
        Task<PortfolioViewModel> GetPortfolio(Guid portfolioId);
        Task<SharesViewModel> CreateShare(Guid portfolioId, string name, decimal totalValue, int count);
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PortfolioService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PortfolioViewModel> CreatePortfolio(Guid userId)
        {
            var shareControlServiceDNS = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_DNS");
            if (string.IsNullOrEmpty(shareControlServiceDNS))
                throw new NullReferenceException("SHARECONTROL_SERVICE_DNS url is null");
            var shareControlServicePORT = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_PORT");
            if (string.IsNullOrEmpty(shareControlServicePORT))
                throw new NullReferenceException("SHARECONTROL_SERVICE_PORT url is null");

            var requestUri = "http://" + shareControlServiceDNS + ":" + shareControlServicePORT + $"/api/Portfolio";
            var request = HttpRequestPost(requestUri,
            new
            {
                UserId = userId,
            },
            out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PortfolioViewModel>();
            return null;
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

        public async Task<PortfolioViewModel> GetPortfolio(Guid portfolioId)
        {
            var shareControlServiceDNS = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_DNS");
            if (string.IsNullOrEmpty(shareControlServiceDNS))
                throw new NullReferenceException("SHARECONTROL_SERVICE_DNS url is null");
            var shareControlServicePORT = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_PORT");
            if (string.IsNullOrEmpty(shareControlServicePORT))
                throw new NullReferenceException("SHARECONTROL_SERVICE_PORT url is null");

            var requestUri = "http://" + shareControlServiceDNS + ":" + shareControlServicePORT + $"/api/Portfolio/{portfolioId}";

            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PortfolioViewModel>();
            return null;
        }

        public async Task<SharesViewModel> CreateShare(Guid portfolioId, string name, decimal totalValue, int count)
        {
            var shareControlServiceDNS = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_DNS");
            if (string.IsNullOrEmpty(shareControlServiceDNS))
                throw new NullReferenceException("SHARECONTROL_SERVICE_DNS url is null");
            var shareControlServicePORT = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_PORT");
            if (string.IsNullOrEmpty(shareControlServicePORT))
                throw new NullReferenceException("SHARECONTROL_SERVICE_PORT url is null");

            var requestUri = "http://" + shareControlServiceDNS + ":" + shareControlServicePORT + $"/api/Portfolio/{portfolioId}/shares";
            var request = HttpRequestPost(requestUri,
            new
            {
                Name = name,
                TotalValue = totalValue,
                TotalCount = count
            },
            out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<SharesViewModel>();
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



        public class PortfolioViewModel
        {
            public Guid PortfolioId { get; set; }
            public Guid OwnerId { get; set; }
            public ICollection<SharesViewModel> Shares { get; set; }
        }

        public class SharesViewModel
        {
            public Guid ShareId { get; set; }
            public int Count { get; set; }
            public Guid PortfolioId { get; set; }
        }
    }
}