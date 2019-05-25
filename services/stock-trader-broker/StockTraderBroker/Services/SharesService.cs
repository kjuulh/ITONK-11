using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTraderBroker.Services
{
    public interface ISharesService
    {
        Task<SharesService.SingleShareViewModel> GetShareValue(Guid shareId);
    }

    public class SharesService : ISharesService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SharesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SingleShareViewModel> GetShareValue(Guid shareId)
        {
            var sharesServiceDNS = Environment.GetEnvironmentVariable("SHARES_SERVICE_DNS");
            if (string.IsNullOrEmpty(sharesServiceDNS))
                throw new NullReferenceException("SHARES_SERVICE_DNS url is null");
            var sharesServicePORT = Environment.GetEnvironmentVariable("SHARES_SERVICE_PORT");
            if (string.IsNullOrEmpty(sharesServicePORT))
                throw new NullReferenceException("SHARES_SERVICE_PORT url is null");

            var requestUri = "http://" + sharesServiceDNS + ":" + sharesServicePORT + "/api/Shares/" + shareId;

            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<SingleShareViewModel>();

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

        public class SingleShareViewModel
        {
            public Decimal SingleShareValue { get; set; }
        }
    }
}