using System;
using System.Collections.Generic;
using Account.Models;
using Account.ViewModels;

namespace Account.Services {
    public interface IAccountService {
        User Get (Guid id);
        Guid Register (UserViewModel userViewModel);
        IEnumerable<User> GetAll ();
        void Delete (Guid id);
        void Update (User user);
        User Get (string email);
    }
}