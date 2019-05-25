using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockTraderBroker.Database;
using StockTraderBroker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace StockTraderBroker.Repositories
{
    public interface IUsersRepository
    {
        User Get(Guid userId);
        Task<User> GetAsync(Guid userId);
        IEnumerable<User> GetAll();
        IAsyncEnumerable<User> GetAllAsync();
        void Create(User user);
        void Update(User user);
        void Delete(Guid userId);
        Task DeleteAsync(Guid id);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly StockTraderBrokerContext _context;
        private readonly DbSet<User> _usersEntity;

        public UsersRepository(StockTraderBrokerContext context)
        {
            _context = context;
            _usersEntity = context.Set<User>();
        }

        public User Get(Guid userId)
        {
            return GetAsync(userId).Result;
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await _usersEntity.SingleOrDefaultAsync(user => user.UserId == userId);
        }

        public IEnumerable<User> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<User> GetAllAsync()
        {
            return _usersEntity.AsAsyncEnumerable();
        }

        public void Create(User user)
        {
            _context.Entry(user).State = EntityState.Added;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(Guid userId)
        {
            var userToDelete = _usersEntity.SingleOrDefault(user => user.UserId == userId);
            if (userToDelete != null) _usersEntity.Remove(userToDelete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var usersToDelete = await GetAsync(id);
            if (usersToDelete != null) _usersEntity.Remove(usersToDelete);
        }
    }
}