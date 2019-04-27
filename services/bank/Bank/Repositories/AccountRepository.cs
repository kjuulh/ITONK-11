using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Bank.Database;
using Bank.Models;
using Bank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Bank.Repositories {
    public class BankRepository : IBankRepository {
        private readonly BankContext _context;
        private readonly DbSet<User> _bankEntity;

        public BankRepository (BankContext context) {
            _context = context;
            _bankEntity = context.Set<User> ();
        }

        public User Get (Guid id) {
            return GetAsync (id).Result;
        }

        public async Task<User> GetAsync (Guid id) {
            return await _bankEntity.SingleOrDefaultAsync (user => user.UserId == id);
        }

        public IEnumerable<User> GetAll () {
            return GetAllAsync ().ToEnumerable ();
        }

        public IAsyncEnumerable<User> GetAllAsync () {
            return _bankEntity.AsAsyncEnumerable ();
        }

        public void Register (User user) {
            _context.Entry (user).State = EntityState.Added;
        }

        public void Update (User user) {
            _context.Entry (user).State = EntityState.Modified;
        }

        public void Delete (Guid id) {
            var userToDelete = _bankEntity.SingleOrDefault (user => user.UserId == id);
            if (userToDelete != null) _bankEntity.Remove (userToDelete);
        }

        public async Task DeleteAsync (Guid id) {
            var userToDelete = await GetAsync (id);
            if (userToDelete != null) _bankEntity.Remove (userToDelete);
        }

        public Task<User> GetAsync (string email) {
            return _bankEntity.SingleOrDefaultAsync (user => user.Email == email);
        }
    }
}