using System;
using System.Collections.Generic;
using Bank.Models;
using Bank.ViewModels;

namespace Bank.Services {
    public interface IBankService {
        User Get (Guid id);
        Guid Register (UserViewModel userViewModel);
        IEnumerable<User> GetAll ();
        void Delete (Guid id);
        void Update (User user);
        User Get (string email);
    }
}