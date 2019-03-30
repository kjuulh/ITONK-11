using System;
using System.Collections.Generic;
using System.Linq;
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

        public User Get(Guid id)
        {
            return _unitOfWork.UsersRepository.GetAsync(id).Result;
        }

        public Guid Register(UserViewModel userViewModel)
        {
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = userViewModel.Email,
                DateAdded = DateTime.UtcNow
            };
            
            _unitOfWork.UsersRepository.Register(user);
            _unitOfWork.CommitAsync();
            return user.UserId;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UsersRepository.GetAllAsync().ToEnumerable();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.UsersRepository.Delete(id);
            _unitOfWork.CommitAsync();
        }

        public void Update(User user)
        {
            _unitOfWork.UsersRepository.Update(user);
            _unitOfWork.CommitAsync();
        }

        public User Get(string email)
        {
            return _unitOfWork.UsersRepository.GetAsync(email).Result;
        }
    }
}