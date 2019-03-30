using System;
using System.Collections.Generic;
using Users.Models;
using Users.ViewModels;

namespace Users.Services
{
    public interface IUsersService
    {
        User Get(Guid id);
        Guid Register(UserViewModel userViewModel);
        IEnumerable<User> GetAll();
        void Delete(Guid id);
        void Update(User user);
        User Get(string email);
    }
}