using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TobinTaxer.ViewModels;
using System.Net.Http;

namespace TobinTaxer.Services
{
    public class TraderService : ITraderService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TraderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransactionViewModel> GetTransaction(DateTime dateAdded)
        {
            var transactionServiceDNS = Environment.GetEnvironmentVariable("STOCK_TRADER_BROKER_DNS");
            if (string.IsNullOrEmpty(transactionServiceDNS))
                throw new NullReferenceException("STOCK_TRADER_BROKER_DNS url is null");
            var transactionServicePORT = Environment.GetEnvironmentVariable("STOCK_TRADER_BROKER_PORT");
            if (string.IsNullOrEmpty(transactionServicePORT))
                throw new NullReferenceException("STOCK_TRADER_BROKER_PORT url is null");

            var requestUri = "http://" + transactionServiceDNS + ":" + transactionServicePORT + "/api/transaction/" + dateAdded;

            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<TransactionViewModel>();

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
    }
}
