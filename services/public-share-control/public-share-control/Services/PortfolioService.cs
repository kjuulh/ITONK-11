using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PublicShareControl.Database;
using PublicShareControl.Models;

namespace PublicShareControl.Services
{
    public interface IPortfolioService
    {
        Task<Portfolio> Get(Guid portfolioId);
        IAsyncEnumerable<Portfolio> GetAll();
        void Delete(Guid portfolioId);
        Task<Portfolio> CreatePortfolio(Guid userId);
        Task<Portfolio> GetByUser(Guid userId);
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IUsersService _usersService;
        private readonly UnitOfWork _unitOfWork;

        public PortfolioService(IUnitOfWork unitOfWork, IUsersService usersService)
        {
            _usersService = usersService;
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public Task<Portfolio> Get(Guid portfolioId)
        {
            return _unitOfWork.PortfolioRepository.GetAsync(portfolioId);
        }

        public IAsyncEnumerable<Portfolio> GetAll()
        {
            return _unitOfWork.PortfolioRepository.GetAllAsync();
        }

        public void Delete(Guid portfolioId)
        {
            _unitOfWork.PortfolioRepository.Delete(portfolioId);
        }

        public async Task<Portfolio> CreatePortfolio(Guid userId)
        {
            if (await _usersService.GetUser(userId) == null)
                return null;
            
            var portfolio = new Portfolio
            {
                OwnerId = userId,
                Shares = new List<Share>()
            };

            var portfolioSot = await _unitOfWork.PortfolioRepository.GetByUserIdAsync(userId);
            if (portfolioSot == null)
            {
                _unitOfWork.PortfolioRepository.CreatePortfolio(portfolio);
                await _unitOfWork.CommitAsync();
                return portfolio;
            }
            return portfolioSot;
        }

        public async Task<Portfolio> GetByUser(Guid userId)
        {
            return await _unitOfWork.PortfolioRepository.GetByUserIdAsync(userId);
        }
    }
}