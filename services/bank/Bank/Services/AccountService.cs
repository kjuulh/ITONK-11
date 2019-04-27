using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Database;
using Bank.Models;
using Bank.Repositories;
using Bank.ViewModels;

namespace Bank.Services {
    public class BankService : IBankService {
        private readonly UnitOfWork _unitOfWork;

        public BankService (IUnitOfWork unitOfWork) {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public User Get (Guid id) {
            return _unitOfWork.BankRepository.GetAsync (id).Result;
        }

        public Guid Register (UserViewModel userViewModel) {
            var user = new User () {
                UserId = Guid.NewGuid (),
                Email = userViewModel.Email,
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