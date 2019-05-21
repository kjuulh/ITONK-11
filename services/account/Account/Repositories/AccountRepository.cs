using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Account.Repositories
{
    public interface IAccountRepository
    {
        Models.Account Get(Guid id);
        Task<Models.Account> GetAsync(Guid id);
        IEnumerable<Models.Account> GetAll();
        IAsyncEnumerable<Models.Account> GetAllAsync();
        void Register(Models.Account account);
        void Update(Models.Account account);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly DbSet<Models.Account> _accountEntity;
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context)
        {
            _context = context;
            _accountEntity = context.Set<Models.Account>();
        }

        public Models.Account Get(Guid id)
        {
            return GetAsync(id).Result;
        }

        public async Task<Models.Account> GetAsync(Guid id)
        {
            return await _accountEntity.SingleOrDefaultAsync(account => account.AccountId == id);
        }

        public IEnumerable<Models.Account> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<Models.Account> GetAllAsync()
        {
            return _accountEntity.AsAsyncEnumerable();
        }

        public void Register(Models.Account account)
        {
            _context.Entry(account).State = EntityState.Added;
        }

        public void Update(Models.Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var accountToDelete = _accountEntity.SingleOrDefault(account => account.AccountId == id);
            if (accountToDelete != null) _accountEntity.Remove(accountToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var accountToDelete = await GetAsync(id);
            if (accountToDelete != null) _accountEntity.Remove(accountToDelete);
        }
    }
}