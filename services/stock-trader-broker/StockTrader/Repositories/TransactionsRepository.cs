using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using StockTrader.Database;
using StockTrader.Models;

namespace StockTrader.Repositories {
    public interface ITransactionsRepository {
        Task<Transaction> Get (Guid id);
        Task<Transaction> GetAsync (Guid id);
        IEnumerable<Transaction> GetAll (Guid stockTraderId);
        IAsyncEnumerable<Transaction> GetAllAsync (Guid stockTraderId);
        void Create (Transaction transaction);
    }

    public class TransactionsRepository : ITransactionsRepository {
        private readonly StockTraderContext _context;
        private readonly DbSet<Transaction> _transactionsEntity;

        public TransactionsRepository (StockTraderContext context) {
            _context = context;
            _transactionsEntity = context.Set<Transaction> ();
        }

        public async Task<Transaction> Get (Guid id) {
            return await GetAsync (id);
        }

        public async Task<Transaction> GetAsync (Guid id) {
            return await _transactionsEntity.SingleOrDefaultAsync (transaction => transaction.TransactionId == id);
        }

        public IEnumerable<Transaction> GetAll (Guid stockTraderId) {
            return GetAllAsync (stockTraderId).ToEnumerable ();
        }

        public IAsyncEnumerable<Transaction> GetAllAsync (Guid stockTraderId) {
            return _transactionsEntity.Where (e => e.StockTrader.StockTraderId == stockTraderId)
                .OrderBy (e => e.DateAdded)
                .AsAsyncEnumerable ();
        }

        public void Create (Transaction transaction) {
            _context.Entry (transaction).State = EntityState.Added;
        }
    }
}