using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using PublicShareControl.Models;

namespace PublicShareControl.Repositories
{
    public interface ISharesRepository
    {
        IEnumerable<Share> GetAll();
        IEnumerable<Share> GetShares(Guid shareId);
        Share GetShareByPortfolio(Guid portfolioId, Guid shareId);
        Task<Share> GetShareByPortfolioAsync(Guid portfolioId, Guid shareId);
        void Update(Share share);
        void Add(Share share);
        Task DeleteShareByPortfolioAsync(Guid portfolioId, Guid shareId);
        void DeleteShare(Guid shareId);
        IAsyncEnumerable<Share> GetAllByPortfolio(Guid portfolioId);
    }

    public class SharesRepository : ISharesRepository
    {
        private readonly PSCContext _context;
        private readonly DbSet<Share> _sharesEntity;


        public SharesRepository(PSCContext context)
        {
            _context = context;
            _sharesEntity = context.Set<Share>();
        }

        public IEnumerable<Share> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        private IAsyncEnumerable<Share> GetAllAsync()
        {
            return _sharesEntity.ToAsyncEnumerable();
        }

        public IEnumerable<Share> GetShares(Guid shareId)
        {
            return GetSharesAsync(shareId).ToEnumerable();
        }

        private IAsyncEnumerable<Share> GetSharesAsync(Guid shareId)
        {
            return _sharesEntity
                .Include(e => e.Portfolio)
                .Where(share => share.ShareId == shareId)
                .AsAsyncEnumerable();
        }
        
        public Share GetShareByPortfolio(Guid portfolioId, Guid shareId)
        {
            return GetShareByPortfolioAsync(portfolioId, shareId).Result;
        }

        public async Task<Share> GetShareByPortfolioAsync(Guid portfolioId, Guid shareId)
        {
            return await _sharesEntity
                .Include(e => e.Portfolio)
                .SingleOrDefaultAsync(share => share.Portfolio.PortfolioId == portfolioId && share.ShareId == shareId);
        }

        public void Update(Share share)
        {
            _context.Entry(share).State = EntityState.Modified;
        }
        
        public void Add(Share share)
        {
            _context.Entry(share).State = EntityState.Added;
        }

        public async Task DeleteShareByPortfolioAsync(Guid portfolioId, Guid shareId)
        {
            var portfolioToDelete = await GetShareByPortfolioAsync(portfolioId, shareId);
            if (portfolioToDelete != null) 
                _sharesEntity.Remove(portfolioToDelete);
        }
        
        public void DeleteShare(Guid shareId)
        {
            var portfolioToDelete = GetShares(shareId);
            if (portfolioToDelete.Count() != 0) 
                _sharesEntity.RemoveRange(portfolioToDelete);
        }

        public IAsyncEnumerable<Share> GetAllByPortfolio(Guid portfolioId)
        {
            return _sharesEntity.Include(e => e.Portfolio).Where(share => share.Portfolio.PortfolioId == portfolioId).AsAsyncEnumerable();
        }
    }
}