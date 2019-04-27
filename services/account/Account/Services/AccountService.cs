using System;
using System.Collections.Generic;
using System.Linq;
using Account.Database;
using Account.Models;
using Account.Repositories;
using Account.ViewModels;

namespace Account.Services {
    public class AccountService : IAccountService {
        private readonly UnitOfWork _unitOfWork;

        public AccountService (IUnitOfWork unitOfWork) {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public User Get (Guid id) {
            return _unitOfWork.AccountRepository.GetAsync (id).Result;
        }

        public Guid Register (UserViewModel userViewModel) {
            var user = new User () {
                UserId = Guid.NewGuid (),
                Email = userViewModel.Email,
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.AccountRepository.Register (user);
            _unitOfWork.CommitAsync ();
            return user.UserId;
        }

        public IEnumerable<User> GetAll () {
            return _unitOfWork.AccountRepository.GetAllAsync ().ToEnumerable ();
        }

        public void Delete (Guid id) {
            _unitOfWork.AccountRepository.Delete (id);
            _unitOfWork.CommitAsync ();
        }

        public void Update (User user) {
            _unitOfWork.AccountRepository.Update (user);
            _unitOfWork.CommitAsync ();
        }

        public User Get (string email) {
            return _unitOfWork.AccountRepository.GetAsync (email).Result;
        }
    }
}