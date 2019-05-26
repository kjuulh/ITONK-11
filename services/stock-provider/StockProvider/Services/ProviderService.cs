using System;
using System.Threading.Tasks;
using StockProvider.ViewModels;

namespace StockProvider.Services
{
    public interface IProviderService
    {
        Task<PortfolioService.SharesViewModel> CreateShare(CreateShareViewModel viewModel);
    }

    public class ProviderService : IProviderService
    {
        private readonly IPortfolioService _portfolioService;

        public ProviderService(IPortfolioService portfolioService)
        {
            this._portfolioService = portfolioService;
        }

        public async Task<PortfolioService.SharesViewModel> CreateShare(CreateShareViewModel viewModel)
        {
            PortfolioService.PortfolioViewModel portfolio = null;

            // Check if portfolioId is correct
            if (viewModel.PortfolioId != null && viewModel.PortfolioId != Guid.Empty)
            {
                portfolio = await _portfolioService.GetPortfolio(viewModel.PortfolioId);
                if (portfolio == null)
                    throw new ArgumentException("PortfolioId doesn't match any existing portfolios");
            }
            else
            {
                portfolio = await _portfolioService.GetPortfolioByUser(viewModel.UserId);
                if (portfolio == null)
                    portfolio = await _portfolioService.CreatePortfolio(viewModel.UserId);
                viewModel.PortfolioId = portfolio.PortfolioId;
            }

            var share = await _portfolioService.CreateShare(viewModel.PortfolioId, viewModel.Name, viewModel.TotalValue, viewModel.Count);
            if (share == null)
                throw new ArgumentException("Couldn't create share");

            return share;
        }
    }
}