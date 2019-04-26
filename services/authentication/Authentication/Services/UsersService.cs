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
            var requestUri = "http://users-service:80/api/users";
            var request = HttpRequestPost(
                requestUri, new
                { Email = username }, 
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
            var requestUri = "http://users-service:80/api/users/" + id.ToString();
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
            var requestUri = "http://users-service:80/api/users/" + username;
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
            request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8);

            client = _httpClientFactory.CreateClient();
            return request;
        }
    }
}