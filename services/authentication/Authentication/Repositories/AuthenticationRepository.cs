using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Authentication.Database;
using Authentication.Models;
using Authentication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Authentication.Repositories {
    public class AuthenticationRepository : IAuthenticationRepository {
        private readonly AuthenticationContext _context;
        private readonly DbSet<User> _authenticationEntity;

        public AuthenticationRepository (AuthenticationContext context) {
            _context = context;
            _authenticationEntity = context.Set<User> ();
        }

        public User Get (Guid id) {
            return GetAsync (id).Result;
        }

        public async Task<User> GetAsync (Guid id) {
            return await _authenticationEntity.SingleOrDefaultAsync (user => user.UserId == id);
        }

        public IEnumerable<User> GetAll () {
            return GetAllAsync ().ToEnumerable ();
        }

        public IAsyncEnumerable<User> GetAllAsync () {
            return _authenticationEntity.AsAsyncEnumerable ();
        }

        public void Register (User user) {
            _context.Entry (user).State = EntityState.Added;
        }

        public void Update (User user) {
            _context.Entry (user).State = EntityState.Modified;
        }

        public void Delete (Guid id) {
            var userToDelete = _authenticationEntity.SingleOrDefault (user => user.UserId == id);
            if (userToDelete != null) _authenticationEntity.Remove (userToDelete);
        }

        public async Task DeleteAsync (Guid id) {
            var userToDelete = await GetAsync (id);
            if (userToDelete != null) _authenticationEntity.Remove (userToDelete);
        }

        public Task<User> GetAsync (string email) {
            return _authenticationEntity.SingleOrDefaultAsync (user => user.Email == email);
        }
    }
}