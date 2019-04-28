using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Database;
using Users.Models;
using Users.Repositories;
using Users.ViewModels;

namespace Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public async Task<User> Get(Guid id)
        {
            return await _unitOfWork.UsersRepository.GetAsync(id);
        }

        public async Task<Guid> Register(UserViewModel userViewModel)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = userViewModel.Email.ToLower(),
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.UsersRepository.Register(user);
            await _unitOfWork.CommitAsync();
            return user.UserId;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UsersRepository.GetAllAsync().ToEnumerable();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.UsersRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(User user)
        {
            user.Email = user.Email.ToLower();
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.CommitAsync();
        }

        public User Get(string email)
        {
            return _unitOfWork.UsersRepository.GetAsync(email).Result;
        }
    }
}