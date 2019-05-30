using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockSeller.Services
{
  public interface ISellerService
  {
    Task<TraderService.RequestViewModel> SellShare(Guid userId, Guid shareId, int amount);
  }

  public class SellerService : ISellerService
  {
    private readonly IBankService _bankService;
    private readonly IPortfolioService _portfolioService;
    private readonly ITraderService _traderService;

    public SellerService(IBankService bankService,
      IPortfolioService portfolioService,
      ITraderService traderService)
    {
      this._bankService = bankService;
      this._portfolioService = portfolioService;
      this._traderService = traderService;
    }

    public async Task<TraderService.RequestViewModel> SellShare(Guid userId, Guid shareId, int amount)
    {
      try
      {
        var accounts = await _bankService.GetAccounts(userId);
        var portfolio = await _portfolioService.GetPortfolioByUser(userId);

        if (accounts.Accounts.FirstOrDefault() == null || portfolio == null)
          throw new ArgumentException("Either account or portfolio wasn't found");

        return await _traderService.SellShare(accounts.Accounts.FirstOrDefault().AccountId,
          portfolio.PortfolioId,
          shareId,
          amount);
      }
      catch (System.Exception)
      {
        return null;
      }
    }
  }
}