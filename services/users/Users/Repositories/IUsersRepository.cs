using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Repositories
{
    public interface IUsersRepository
    {
        User Get(Guid id);
        Task<User> GetAsync(Guid id);
        IEnumerable<User> GetAll();
        IAsyncEnumerable<User> GetAllAsync();
        void Register(User user);
        void Update(User user);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        Task<User> GetAsync(string email);
    }
}