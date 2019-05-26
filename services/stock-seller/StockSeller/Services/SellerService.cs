using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockSeller.Services
{
  public interface ISellerService
  {
    Task BuyStock(Guid requestId, Guid userId);
  }

  public class SellerService : ISellerService
  {
    private readonly IBankService _bankService;
    private readonly IPortfolioService _portfolioService;

    public SellerService(IBankService bankService, IPortfolioService portfolioService)
    {
      this._bankService = bankService;
      this._portfolioService = portfolioService;
    }

    public async Task BuyStock(Guid requestId, Guid userId)
    {
      try
      {
        var accounts = await _bankService.GetAccounts(userId);
        var portfolio = await _portfolioService.GetPortfolioByUser(userId);

        if (accounts.Accounts.FirstOrDefault() == null || portfolio == null)
          throw new ArgumentException("Either account or portfolio wasn't found");

        return await _traderService.BuyShare(requestId, accounts.Accounts.FirstOrDefault(), portfolio.PortfolioId);
      }
      catch (System.Exception)
      {

        throw;
      }

      //TODO: Get portfolio from userId
      //TODO: Make request
    }
  }
}