using System;
using System.Linq;
using System.Threading.Tasks;
using StockSeller.Services;

namespace StockBuyer.Services
{

    public interface IBuyerService
    {
        Task<TraderService.RequestViewModel> BuyStock(Guid requestId, Guid userId);
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

        public async Task<TraderService.RequestViewModel> BuyStock(Guid requestId, Guid userId)
        {
            try
            {
                var accounts = await _bankService.GetAccounts(userId);
                var portfolio = await _portfolioService.GetPortfolioByUser(userId);
           

                if (accounts.Accounts.FirstOrDefault() == null || portfolio == null)
                    throw new ArgumentException("Either account or portfolio wasn't found");

                return await _traderService.BuyShare(requestId, portfolio.PortfolioId);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
