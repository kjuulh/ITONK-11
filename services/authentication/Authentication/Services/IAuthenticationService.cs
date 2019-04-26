using System;
using System.Collections.Generic;
using Authentication.Models;
using Authentication.ViewModels;

namespace Authentication.Services {
    public interface IAuthenticationService {
        User Get (Guid id);
        Guid Register (UserViewModel userViewModel);
        IEnumerable<User> GetAll ();
        void Delete (Guid id);
        void Update (User user);
        User Get (string email);
    }
}