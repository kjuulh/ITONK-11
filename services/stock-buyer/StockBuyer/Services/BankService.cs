using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockBuyer.Services
{
  public interface IBankService
  {
    Task<BankService.BankAccount> GetAccounts(Guid userId);
  }
  public class BankService : IBankService
  {
    private readonly IHttpClientFactory _httpClientFactory;

    public BankService(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    public async Task<BankAccount> GetAccounts(Guid userId)
    {
      var serviceDNS = Environment.GetEnvironmentVariable("BANK_SERVICE_DNS");
      if (string.IsNullOrEmpty(serviceDNS))
        throw new NullReferenceException("BANK_SERVICE_DNS url is null");
      var servicePORT = Environment.GetEnvironmentVariable("BANK_SERVICE_PORT");
      if (string.IsNullOrEmpty(servicePORT))
        throw new NullReferenceException("BANK_SERVICE_PORT url is null");

      var requestUri = "http://" + serviceDNS + ":" + servicePORT + "/api/Bank/" + userId;
      var request = HttpRequestGet(requestUri, out var client);

      var response = await client.SendAsync(request);

      if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<BankAccount>();

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

    public class BankAccount
    {
      public Guid UserId { get; set; }
      public ICollection<Account> Accounts { get; set; }

    }
    public class Account
    {
      public Guid AccountId { get; set; }
    }
  }
}