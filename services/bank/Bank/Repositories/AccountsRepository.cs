using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Database;
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Bank.Repositories
{
    public interface IAccountsRepository
    {
        Account Get(Guid accountId);
        Task<Account> GetAsync(Guid accountId);
        IAsyncEnumerable<Account> GetByUserId(Guid userId);
        IQueryable<Account> GetByUserIdAsync(Guid userId);
        IEnumerable<Account> GetAll();
        IAsyncEnumerable<Account> GetAllAsync();
        void Create(Account account);
        void Update(Account account);
        void Delete(Guid accountId);
        Task DeleteAsync(Guid id);
    }

    public class AccountsRepository : IAccountsRepository
    {
        private readonly DbSet<Account> _accountsEntity;
        private readonly BankContext _context;

        public AccountsRepository(BankContext context)
        {
            _context = context;
            _accountsEntity = context.Set<Account>();
        }

        public Account Get(Guid accountId)
        {
            return GetAsync(accountId).Result;
        }

        public async Task<Account> GetAsync(Guid accountId)
        {
            return await _accountsEntity.SingleOrDefaultAsync(account => account.AccountId == accountId);
        }

        public IAsyncEnumerable<Account> GetByUserId(Guid userId)
        {
            return GetByUserIdAsync(userId).ToAsyncEnumerable();
        }

        public IQueryable<Account> GetByUserIdAsync(Guid userId)
        {
            return _accountsEntity.Where(account => account.User.UserId == userId);
        }

        public IEnumerable<Account> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<Account> GetAllAsync()
        {
            return _accountsEntity.AsAsyncEnumerable();
        }

        public void Create(Account account)
        {
            _context.Entry(account).State = EntityState.Added;
        }

        public void Update(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
        }

        public void Delete(Guid accountId)
        {
            var accountToDelete = _accountsEntity.SingleOrDefault(account => account.AccountId == accountId);
            if (accountToDelete != null) _accountsEntity.Remove(accountToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var accountsToDelete = await GetAsync(id);
            if (accountsToDelete != null) _accountsEntity.Remove(accountsToDelete);
        }
    }
}