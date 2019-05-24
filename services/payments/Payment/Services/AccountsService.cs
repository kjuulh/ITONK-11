using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Payment.Services
{
    public interface IAccountsService
    {
        Task<AccountsService.TransactionViewModel> CreateTransaction(Guid accountId, decimal amount);
        Task<AccountsService.TransactionViewModel> RevertTransaction(Guid accountId, Guid transactionId);
    }

    public class AccountsService : IAccountsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransactionViewModel> CreateTransaction(Guid accountId, decimal amount)
        {
            var accountsServiceDns = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_DNS");
            if (string.IsNullOrEmpty(accountsServiceDns))
                throw new NullReferenceException("ACCOUNTS_SERVICE_DNS url is null");
            var accountsServicePort = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_PORT");
            if (string.IsNullOrEmpty(accountsServicePort))
                throw new NullReferenceException("ACCOUNTS_SERVICE_PORT url is null");
            var requestUri = "http://" + accountsServiceDns + ":" + accountsServicePort + $"/api/Account/{accountId}/transactions";
            var request = HttpRequestPost(
                requestUri,
                new
                {
                    Amount = amount
                },
                out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsAsync<string>();
                return new TransactionViewModel {TransactionId = Guid.Parse(responseAsString)};
            }

            return null;
        }

        public async Task<TransactionViewModel> RevertTransaction(Guid accountId, Guid transactionId)
        {
            var accountsServiceDns = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_DNS");
            if (string.IsNullOrEmpty(accountsServiceDns))
                throw new NullReferenceException("ACCOUNTS_SERVICE_DNS url is null");
            var accountsServicePort = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_PORT");
            if (string.IsNullOrEmpty(accountsServicePort))
                throw new NullReferenceException("ACCOUNTS_SERVICE_PORT url is null");
            var requestUri = "http://" + accountsServiceDns + ":" + accountsServicePort + $"/api/Account/{accountId}/transactions/{transactionId}";
            var request = HttpRequestPut(
                requestUri,
                new {},
                out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsAsync<string>();
                return new TransactionViewModel {TransactionId = Guid.Parse(responseAsString)};
            }

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
        
        public class TransactionViewModel
        {
            public Guid TransactionId { get; set; }
        }
    }
}