using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Database;
using Account.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Account.Repositories
{
    public interface ITransactionsRepository
    {
        Task<Transaction> Get(Guid id);
        Task<Transaction> GetAsync(Guid id);
        IEnumerable<Transaction> GetAll(Guid accountId);
        IAsyncEnumerable<Transaction> GetAllAsync(Guid accountId);
        void Create(Transaction transaction);
    }

    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AccountContext _context;
        private readonly DbSet<Transaction> _transactionsEntity;

        public TransactionsRepository(AccountContext context)
        {
            _context = context;
            _transactionsEntity = context.Set<Transaction>();
        }

        public async Task<Transaction> Get(Guid id)
        {
            return await GetAsync(id);
        }

        public async Task<Transaction> GetAsync(Guid id)
        {
            return await _transactionsEntity.SingleOrDefaultAsync(transaction => transaction.TransactionId == id);
        }

        public IEnumerable<Transaction> GetAll(Guid accountId)
        {
            return GetAllAsync(accountId).ToEnumerable();
        }

        public IAsyncEnumerable<Transaction> GetAllAsync(Guid accountId)
        {
            return _transactionsEntity.Where(e => e.Account.AccountId == accountId)
                .OrderBy(e => e.DateAdded)
                .AsAsyncEnumerable();
        }

        public void Create(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Added;
        }
    }
}