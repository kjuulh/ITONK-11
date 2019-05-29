using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockSeller.Services
{
  public interface ISellerService
  {
    Task SellStock(Guid requestId, Guid userId, int amount);
  }

  public class SellerService : ISellerService
  {
    private readonly IBankService _bankService;
    private readonly IPortfolioService _portfolioService;
    private readonly ITraderService _traderService;

    public SellerService(IBankService bankService, IPortfolioService portfolioService, ITraderService traderService)
    {
      this._bankService = bankService;
      this._portfolioService = portfolioService;
      this._traderService = traderService;
    }

    public async Task SellStock(Guid requestId, Guid userId, int amount)
    {
      try
      {
        var accounts = await _bankService.GetAccounts(userId);
        var portfolio = await _portfolioService.GetPortfolioByUser(userId);

        if (accounts.Accounts.FirstOrDefault() == null || portfolio == null)
          throw new ArgumentException("Either account or portfolio wasn't found");

        return await _traderService.SellShare(requestId, accounts.Accounts.FirstOrDefault(), portfolio.PortfolioId, amount);
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