using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using TobinTaxer.Database;
using TobinTaxer.Models;

namespace TobinTaxer.Repositories
{
    public class TobinTaxerRepository : ITobinTaxerRepository
    {
        private readonly TobinTaxerContext _context;
        private readonly DbSet<Transaction> _TobinTaxerEntity;

        public TobinTaxerRepository(TobinTaxerContext context)
        {
            _context = context;
            _TobinTaxerEntity = context.Set<Transaction>();
        }

        public Transaction Get(Guid id)
        {
            return GetAsync(id).Result;
        }

        public async Task<Transaction> GetAsync(Guid id)
        {
            return await _TobinTaxerEntity.SingleOrDefaultAsync(transaction => transaction.TransactionId == id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<Transaction> GetAllAsync()
        {
            return _TobinTaxerEntity.AsAsyncEnumerable();
        }

        public void Register(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Added;
        }

        public void Update(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var transactionToDelete = _TobinTaxerEntity.SingleOrDefault(transaction => transaction.TransactionId == id);
            if (transactionToDelete != null) _TobinTaxerEntity.Remove(transactionToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var transactionToDelete = await GetAsync(id);
            if (transactionToDelete != null) _TobinTaxerEntity.Remove(transactionToDelete);
        }

        public Task<Transaction> GetAsync(DateTime timestamp)
        {
            return _TobinTaxerEntity.SingleOrDefaultAsync(transaction => transaction.DateTaxed == timestamp);
        }
    }
}