using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTraderBroker.Services
{
    public interface IShareControlService
    {
        Task<bool> ChangeOwnershipOfShare(Guid shareId, Guid ownerPortfolioId, Guid receiverPortfolioId, int count);
        Task<bool> HasEnoughShares(Guid portfolioId, Guid shareId, int amount);
    }

    public class ShareControlService : IShareControlService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShareControlService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> ChangeOwnershipOfShare(Guid shareId, Guid ownerPortfolioId, Guid receiverPortfolioId, int count)
        {
            var shareControlServiceDNS = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_DNS");
            if (string.IsNullOrEmpty(shareControlServiceDNS))
                throw new NullReferenceException("SHARECONTROL_SERVICE_DNS url is null");
            var shareControlServicePORT = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_PORT");
            if (string.IsNullOrEmpty(shareControlServicePORT))
                throw new NullReferenceException("SHARECONTROL_SERVICE_PORT url is null");

            var requestUri = "http://" + shareControlServiceDNS + ":" + shareControlServicePORT + $"/api/Portfolio/shares";
            var request = HttpRequestPut(requestUri,
            new
            {
                ShareId = shareId,
                OwnerPortfolioId = ownerPortfolioId,
                ReceiverPortfolioId = receiverPortfolioId,
                Count = count
            },
            out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) return true;

            return false;
        }

        public async Task<bool> HasEnoughShares(Guid portfolioId, Guid shareId, int amount)
        {
            var shareControlServiceDNS = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_DNS");
            if (string.IsNullOrEmpty(shareControlServiceDNS))
                throw new NullReferenceException("SHARECONTROL_SERVICE_DNS url is null");
            var shareControlServicePORT = Environment.GetEnvironmentVariable("SHARECONTROL_SERVICE_PORT");
            if (string.IsNullOrEmpty(shareControlServicePORT))
                throw new NullReferenceException("SHARECONTROL_SERVICE_PORT url is null");

            var requestUri = "http://" + shareControlServiceDNS + ":" + shareControlServicePORT + $"/api/Portfolio/{portfolioId}/shares/{shareId}";

            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var countViewModel = await response.Content.ReadAsAsync<SharesViewModel>();
                if (countViewModel.Count > amount)
                    return true;
            }
            return false;
        }


        private HttpRequestMessage HttpRequestGet(string requestUri, out HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                requestUri);
            request.Headers.Add("Accept", "application/json");

            client = _httpClientFactory.CreateClient();
            return request;
        }

        private HttpRequestMessage HttpRequestPut(string requestUri, object content, out HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                requestUri);
            request.Headers.Add("Accept", "application/json");

            var jsonContent = JsonConvert.SerializeObject(content, Formatting.None).ToLower();

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            client = _httpClientFactory.CreateClient();
            return request;
        }

        private class SharesViewModel
        {
            public int Count { get; set; }
        }
    }
}