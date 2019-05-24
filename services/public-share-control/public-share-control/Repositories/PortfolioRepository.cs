using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using PublicShareControl.Models;

namespace PublicShareControl.Repositories
{
    public interface IPortfolioRepository
    {
        Portfolio Get(Guid id);
        Task<Portfolio> GetAsync(Guid id);
        Task<Portfolio> GetByUserIdAsync(Guid userId);
        IEnumerable<Portfolio> GetAll();
        IAsyncEnumerable<Portfolio> GetAllAsync();
        void Update(Portfolio portfolio);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        void CreatePortfolio(Portfolio model);
    }

    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PSCContext _context;
        private readonly DbSet<Portfolio> _portfolioEntity;


        public PortfolioRepository(PSCContext context)
        {
            _context = context;
            _portfolioEntity = context.Set<Portfolio>();
        }

        public Portfolio Get(Guid id)
        {
            return GetAsync(id).Result;
        }

        public async Task<Portfolio> GetAsync(Guid id)
        {
            return await _portfolioEntity
                .Include(e => e.Shares)
                .SingleOrDefaultAsync(portfolio => portfolio.PortfolioId == id);
        }
        
        public async Task<Portfolio> GetByUserIdAsync(Guid userId)
        {
            return await _portfolioEntity.Include(e => e.Shares).SingleOrDefaultAsync(portfolio => portfolio.OwnerId == userId);
        }

        public IEnumerable<Portfolio> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<Portfolio> GetAllAsync()
        {
            return _portfolioEntity.Include(e => e.Shares).AsAsyncEnumerable();
        }

        public void Update(Portfolio portfolio)
        {
            _context.Entry(portfolio).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var portfolioToDelete = _portfolioEntity.SingleOrDefault(portfolio => portfolio.PortfolioId == id);
            if (portfolioToDelete != null) _portfolioEntity.Remove(portfolioToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var portfolioToDelete = await GetAsync(id);
            if (portfolioToDelete != null) _portfolioEntity.Remove(portfolioToDelete);
        }

        public void CreatePortfolio(Portfolio model)
        {
            _context.Entry(model).State = EntityState.Added;
        }
    }
}