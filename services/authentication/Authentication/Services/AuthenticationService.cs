using System;
using System.Collections.Generic;
using System.Linq;
using Authentication.Database;
using Authentication.Models;
using Authentication.Repositories;
using Authentication.ViewModels;

namespace Authentication.Services {
    public class AuthenticationService : IAuthenticationService {
        private readonly UnitOfWork _unitOfWork;

        public AuthenticationService (IUnitOfWork unitOfWork) {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public User Get (Guid id) {
            return _unitOfWork.AuthenticationRepository.GetAsync (id).Result;
        }

        public Guid Register (UserViewModel userViewModel) {
            var user = new User () {
                UserId = Guid.NewGuid (),
                Email = userViewModel.Email,
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.AuthenticationRepository.Register (user);
            _unitOfWork.CommitAsync ();
            return user.UserId;
        }

        public IEnumerable<User> GetAll () {
            return _unitOfWork.AuthenticationRepository.GetAllAsync ().ToEnumerable ();
        }

        public void Delete (Guid id) {
            _unitOfWork.AuthenticationRepository.Delete (id);
            _unitOfWork.CommitAsync ();
        }

        public void Update (User user) {
            _unitOfWork.AuthenticationRepository.Update (user);
            _unitOfWork.CommitAsync ();
        }

        public User Get (string email) {
            return _unitOfWork.AuthenticationRepository.GetAsync (email).Result;
        }
    }
}