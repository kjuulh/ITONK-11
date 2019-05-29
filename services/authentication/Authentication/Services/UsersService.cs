using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Authentication.Services
{
    public interface IUsersService
    {
        Task<UsersService.UsersServiceViewModel> RegisterUser(string username);
        Task<UsersService.UsersServiceViewModel> GetUser(Guid id);
        Task<UsersService.UsersServiceViewModel> GetUser(string username);
    }

    public class UsersService : IUsersService
    {
        public class UsersServiceViewModel
        {
            public Guid UserId { get; set; }
            public string Email { get; set; }
        }

        private readonly IHttpClientFactory _httpClientFactory;

        public UsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UsersServiceViewModel> RegisterUser(string username)
        {
            var usersServiceDNS = Environment.GetEnvironmentVariable("USERS_SERVICE_DNS");
            if (string.IsNullOrEmpty(usersServiceDNS)) throw new NullReferenceException("USERS_SERVICE_DNS url is null");
            var usersServicePORT = Environment.GetEnvironmentVariable("USERS_SERVICE_PORT");
            if (string.IsNullOrEmpty(usersServicePORT)) throw new NullReferenceException("USERS_SERVICE_PORT url is null");
            var requestUri = "http://" + usersServiceDNS + ":" + usersServicePORT + "/api/users";
            var request = HttpRequestPost(
                requestUri, new { Email = username },
                out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UsersServiceViewModel>();
            }

            return null;
        }

        public async Task<UsersServiceViewModel> GetUser(Guid id)
        {
            var usersServiceDNS = Environment.GetEnvironmentVariable("USERS_SERVICE_DNS");
            if (string.IsNullOrEmpty(usersServiceDNS)) throw new NullReferenceException("USERS_SERVICE_DNS url is null");
            var usersServicePORT = Environment.GetEnvironmentVariable("USERS_SERVICE_PORT");
            if (string.IsNullOrEmpty(usersServicePORT)) throw new NullReferenceException("USERS_SERVICE_PORT url is null");
            var requestUri = "http://" + usersServiceDNS + ":" + usersServicePORT + "/api/users/" + id.ToString();
            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UsersServiceViewModel>();
            }

            return null;
        }

        public async Task<UsersServiceViewModel> GetUser(string username)
        {
            var usersServiceDNS = Environment.GetEnvironmentVariable("USERS_SERVICE_DNS");
            if (string.IsNullOrEmpty(usersServiceDNS)) throw new NullReferenceException("USERS_SERVICE_DNS url is null");
            var usersServicePORT = Environment.GetEnvironmentVariable("USERS_SERVICE_PORT");
            if (string.IsNullOrEmpty(usersServicePORT)) throw new NullReferenceException("USERS_SERVICE_PORT url is null");
            var requestUri = "http://" + usersServiceDNS + ":" + usersServicePORT + "/api/users/email/" + username;
            var request = HttpRequestGet(requestUri, out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UsersServiceViewModel>();
            }
            else
            {
                return null;
            }
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