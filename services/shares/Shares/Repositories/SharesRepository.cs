using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Shares.Database;
using Shares.Models;
using Shares.Services;

namespace Shares.Repositories
{
    public class SharesRepository : ISharesRepository
    {
        private readonly SharesContext _context;
        private readonly DbSet<Share> _sharesEntity;

        public SharesRepository(SharesContext context)
        {
            _context = context;
            _sharesEntity = context.Set<Share>();
        }

        public Share Get(Guid id)
        {
            return GetAsync(id).Result;
        }

        public async Task<Share> GetAsync(Guid id)
        {
            return await _sharesEntity.SingleOrDefaultAsync(share => share.ShareId == id);
        }

        public Share GetByName(string name)
        {
            return GetByNameAsync(name).Result;
        }

        public async Task<Share> GetByNameAsync(string name)
        {
            return await _sharesEntity.SingleOrDefaultAsync(share => share.Name == name);
        }

        public IEnumerable<Share> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<Share> GetAllAsync()
        {
            return _sharesEntity.AsAsyncEnumerable();
        }

        public void Establish(Share share)
        {
            _context.Entry(share).State = EntityState.Added;
        }

        public void Update(Share share)
        {
            _context.Entry(share).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var shareToDelete = _sharesEntity.SingleOrDefault(share => share.ShareId == id);
            if (shareToDelete != null) _sharesEntity.Remove(shareToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var shareToDelete = await GetAsync(id);
            if (shareToDelete != null) _sharesEntity.Remove(shareToDelete);
        }
    }
}