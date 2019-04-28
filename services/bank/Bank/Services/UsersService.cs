using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bank.Services
{
    public interface IUsersService
    {
        Task<UsersService.UsersServiceViewModel> GetUser(Guid id);
    }

    public class UsersService : IUsersService
    {
        public class UsersServiceViewModel
        {
            public Guid UserId { get; set; }
        }

        private readonly IHttpClientFactory _httpClientFactory;

        public UsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UsersServiceViewModel> GetUser(Guid id)
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
                return await response.Content.ReadAsAsync<UsersServiceViewModel>();
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
    }
}