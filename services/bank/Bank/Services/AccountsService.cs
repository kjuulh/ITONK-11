using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bank.Services
{
    public class AccountsService
    {
        public class AccountsViewModel
        {
            public Guid UserId { get; set; }
            public decimal balance;
        }

        private readonly IHttpClientFactory _httpClientFactory;

        public AccountsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AccountsViewModel> CreateAccount()
        {
            var accountsServiceDNS = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_DNS");
            if (string.IsNullOrEmpty(accountsServiceDNS))
                throw new NullReferenceException("ACCOUNTS_SERVICE_DNS url is null");
            var accountsServicePORT = Environment.GetEnvironmentVariable("ACCOUNTS_SERVICE_PORT");
            if (string.IsNullOrEmpty(accountsServicePORT))
                throw new NullReferenceException("ACCOUNTS_SERVICE_PORT url is null");

            var requestUri = "http://" + accountsServiceDNS + ":" + accountsServicePORT + "/api/accounts";
            var request = HttpRequestPost(requestUri, new {} ,out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AccountsViewModel>();
            }

            return null;
        }


        public async Task<AccountsViewModel> GetAccount(Guid id)
        {
            var usersServiceDNS = Environment.GetEnvironmentVariable("USERS_SERVICE_DNS");
            if (string.IsNullOrEmpty(usersServiceDNS))
                throw new NullReferenceException("USERS_SERVICE_DNS url is null");
            var usersServicePORT = Environment.GetEnvironmentVariable("USERS_SERVICE_PORT");
            if (string.IsNullOrEmpty(usersServicePORT))
                throw new NullReferenceException("USERS_SERVICE_PORT url is null");

            var requestUri = "http://" + usersServiceDNS + ":" + usersServicePORT + "/api/users/" + id;
            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AccountsViewModel>();
            }

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
    }
}