using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Database;
using Bank.Models;

namespace Bank.Services {
    public interface IAccountService
    {
        User Get (Guid id);
        Guid Register (string userViewModel);
        IEnumerable<User> GetAll ();
        void Delete (Guid id);
        void Update (User user);
        User Get (string email);
    }

    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountService (IUnitOfWork unitOfWork) {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public User Get (Guid id) {
            return _unitOfWork.BankRepository.GetAsync (id).Result;
        }

        public Guid Register (string userViewModel) {
            var user = new User () {
                UserId = Guid.NewGuid (),
                Email = userViewModel,
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.BankRepository.Register (user);
            _unitOfWork.CommitAsync ();
            return user.UserId;
        }

        public IEnumerable<User> GetAll () {
            return _unitOfWork.BankRepository.GetAllAsync ().ToEnumerable ();
        }

        public void Delete (Guid id) {
            _unitOfWork.BankRepository.Delete (id);
            _unitOfWork.CommitAsync ();
        }

        public void Update (User user) {
            _unitOfWork.BankRepository.Update (user);
            _unitOfWork.CommitAsync ();
        }

        public User Get (string email) {
            return _unitOfWork.BankRepository.GetAsync (email).Result;
        }
    }
}