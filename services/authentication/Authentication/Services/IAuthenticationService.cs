using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Models;
using Authentication.ViewModels;

namespace Authentication.Services
{
    public interface IAuthenticationService
    {
        User Get(Guid id);
        Task<User> Register(string username, string password);
        Task<string> Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        Task DeleteAsync(Guid id);
        Task UpdateAsync(User user);
        User Get(string email);
    }
}