using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Account.Database;
using Account.Models;
using Account.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Account.Repositories {
    public class AccountRepository : IAccountRepository {
        private readonly AccountContext _context;
        private readonly DbSet<User> _accountEntity;

        public AccountRepository (AccountContext context) {
            _context = context;
            _accountEntity = context.Set<User> ();
        }

        public User Get (Guid id) {
            return GetAsync (id).Result;
        }

        public async Task<User> GetAsync (Guid id) {
            return await _accountEntity.SingleOrDefaultAsync (user => user.UserId == id);
        }

        public IEnumerable<User> GetAll () {
            return GetAllAsync ().ToEnumerable ();
        }

        public IAsyncEnumerable<User> GetAllAsync () {
            return _accountEntity.AsAsyncEnumerable ();
        }

        public void Register (User user) {
            _context.Entry (user).State = EntityState.Added;
        }

        public void Update (User user) {
            _context.Entry (user).State = EntityState.Modified;
        }

        public void Delete (Guid id) {
            var userToDelete = _accountEntity.SingleOrDefault (user => user.UserId == id);
            if (userToDelete != null) _accountEntity.Remove (userToDelete);
        }

        public async Task DeleteAsync (Guid id) {
            var userToDelete = await GetAsync (id);
            if (userToDelete != null) _accountEntity.Remove (userToDelete);
        }

        public Task<User> GetAsync (string email) {
            return _accountEntity.SingleOrDefaultAsync (user => user.Email == email);
        }
    }
}