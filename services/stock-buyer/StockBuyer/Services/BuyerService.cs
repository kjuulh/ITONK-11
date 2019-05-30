using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockBuyer.Services
{

    public interface IBuyerService
    {
        Task<TraderService.ResponseViewModel> BuyStock(Guid requestId, Guid userId);
    }

    public class BuyerService : IBuyerService
    {
        private readonly IBankService _bankService;
        private readonly IPortfolioService _portfolioService;
        private readonly ITraderService _traderService;

        public BuyerService(IBankService bankService, IPortfolioService portfolioService, ITraderService traderService)
        {
            _bankService = bankService;
            _portfolioService = portfolioService;
            _traderService = traderService;
        }

        public async Task<TraderService.ResponseViewModel> BuyStock(Guid requestId, Guid userId)
        {
            try
            {
                var account = (await _bankService.GetAccounts(userId)).Accounts.FirstOrDefault();
                var portfolio = await _portfolioService.GetPortfolioByUser(userId);

                if (account == null || portfolio == null)
                    throw new ArgumentException("Either account or portfolio wasn't found");

                return await _traderService.BuyShare(requestId, account.AccountId, portfolio.PortfolioId);
            }
            catch (System.Exception)
            {
                return null;
            }

        }
    }
}