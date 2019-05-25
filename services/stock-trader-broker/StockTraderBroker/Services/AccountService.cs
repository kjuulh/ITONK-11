using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTraderBroker.Services
{
    public interface IAccountService
    {
        Task<bool> PostTaxes(Guid accountId, decimal amount);
        Task<AccountService.BalanceViewModel> GetBalance(Guid accountId);
    }

    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> PostTaxes(Guid accountId, decimal amount)
        {
            var accountsServiceDNS = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_DNS");
            if (string.IsNullOrEmpty(accountsServiceDNS))
                throw new NullReferenceException("ACCOUNTS_SERVICE_DNS url is null");
            var accountsServicePORT = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_PORT");
            if (string.IsNullOrEmpty(accountsServicePORT))
                throw new NullReferenceException("ACCOUNTS_SERVICE_PORT url is null");

            var requestUri = "http://" + accountsServiceDNS + ":" + accountsServicePORT + $"/api/Account/{accountId}/transactions";
            var request = HttpRequestPost(requestUri, new { Amount = amount }, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) return true;

            return false;
        }


        public async Task<BalanceViewModel> GetBalance(Guid accountId)
        {
            var accountsServiceDNS = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_DNS");
            if (string.IsNullOrEmpty(accountsServiceDNS))
                throw new NullReferenceException("ACCOUNTS_SERVICE_DNS url is null");
            var accountsServicePORT = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_PORT");
            if (string.IsNullOrEmpty(accountsServicePORT))
                throw new NullReferenceException("ACCOUNTS_SERVICE_PORT url is null");

            var requestUri = "http://" + accountsServiceDNS + ":" + accountsServicePORT + "/api/Account/" + accountId;

            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<BalanceViewModel>();

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

        public class BalanceViewModel
        {
            public Decimal Balance { get; set; }
        }
    }
}