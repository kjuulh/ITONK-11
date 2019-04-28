using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Models;
using Users.ViewModels;

namespace Users.Services
{
    public interface IUsersService
    {
        Task<User> Get(Guid id);
        Task<Guid> Register(UserViewModel userViewModel);
        IEnumerable<User> GetAll();
        Task Delete(Guid id);
        Task Update(User user);
        User Get(string email);
    }
}