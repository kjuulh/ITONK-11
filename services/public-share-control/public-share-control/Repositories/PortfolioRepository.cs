using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using PublicShareControl.Models;

namespace PublicShareControl.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PSCContext _context;
        private readonly DbSet<PortfolioModel> _portfolioEntity;


        public PortfolioRepository(PSCContext context)
        {
            _context = context;
            _portfolioEntity = context.Set<PortfolioModel>();
        }

        public PortfolioModel Get(Guid id)
        {
            return GetAsync(id).Result;
        }

        public async Task<PortfolioModel> GetAsync(Guid id)
        {
            return await _portfolioEntity.SingleOrDefaultAsync(portfolio => portfolio.Id == id);
        }

        public IEnumerable<PortfolioModel> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<PortfolioModel> GetAllAsync()
        {
            return _portfolioEntity.AsAsyncEnumerable();
        }

        public void Update(PortfolioModel portfolio)
        {
            _context.Entry(portfolio).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var portfolioToDelete = _portfolioEntity.SingleOrDefault(portfolio => portfolio.Id == id);
            if (portfolioToDelete != null) _portfolioEntity.Remove(portfolioToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var portfolioToDelete = await GetAsync(id);
            if (portfolioToDelete != null) _portfolioEntity.Remove(portfolioToDelete);
        }

        public void CreatePortfolio(PortfolioModel model)
        {
            _context.Entry(model).State = EntityState.Added;
        }
    }
}