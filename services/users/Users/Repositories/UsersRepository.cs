using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Users.Database;
using Users.Models;
using Users.Services;

namespace Users.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;
        private readonly DbSet<User> _usersEntity;

        public UsersRepository(UsersContext context)
        {
            _context = context;
            _usersEntity = context.Set<User>();
        }

        public User Get(Guid id)
        {
            return GetAsync(id).Result;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _usersEntity.SingleOrDefaultAsync(user => user.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<User> GetAllAsync()
        {
            return _usersEntity.AsAsyncEnumerable();
        }

        public void Register(User user)
        {
            _context.Entry(user).State = EntityState.Added;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var userToDelete = _usersEntity.SingleOrDefault(user => user.UserId == id);
            if (userToDelete != null) _usersEntity.Remove(userToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var userToDelete = await GetAsync(id);
            if (userToDelete != null) _usersEntity.Remove(userToDelete);
        }

        public Task<User> GetAsync(string email)
        {
            return _usersEntity.SingleOrDefaultAsync(user => user.Email == email);
        }
    }
}