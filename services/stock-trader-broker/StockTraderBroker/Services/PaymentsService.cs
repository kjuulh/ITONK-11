using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTraderBroker.Services
{
    public interface IPaymentsService
    {
        Task<bool> CreateTransaction(Guid sellerAccountId, Guid buyerAccountId, decimal amount);
    }

    public class PaymentsService : IPaymentsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateTransaction(Guid sellerAccountId, Guid buyerAccountId, decimal amount)
        {
            var paymentsServiceDNS = Environment.GetEnvironmentVariable("PAYMENTS_SERVICE_DNS");
            if (string.IsNullOrEmpty(paymentsServiceDNS))
                throw new NullReferenceException("PAYMENTS_SERVICE_DNS url is null");
            var paymentsServicePORT = Environment.GetEnvironmentVariable("PAYMENTS_SERVICE_PORT");
            if (string.IsNullOrEmpty(paymentsServicePORT))
                throw new NullReferenceException("PAYMENTS_SERVICE_PORT url is null");

            var requestUri = "http://" + paymentsServiceDNS + ":" + paymentsServicePORT + $"/api/Payment/create";
            var request = HttpRequestPost(requestUri, new
            {
                BuyerAccountId = buyerAccountId,
                SellerAccountId = sellerAccountId,
                Amount = amount
            }, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) return true;

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