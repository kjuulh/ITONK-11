using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Database;
using Bank.Models;

namespace Bank.Services
{
    public class BankService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IUsersService _usersService;

        public BankService(IUnitOfWork unitOfWork, IUsersService usersService)
        {
            _usersService = usersService;
            _unitOfWork = (UnitOfWork) unitOfWork;
        }
        
        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UsersRepository.GetAllAsync().ToEnumerable();
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _unitOfWork.UsersRepository.GetAsync(userId);
        }

        public async Task<Guid> ReconcileUser(Guid userId)
        {
            var userSOT = await _usersService.GetUser(userId) ?? throw new ArgumentException("User was not found");

            var user = new User
            {
                UserId = userSOT.UserId,
                Accounts = new List<Account>(),
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.UsersRepository.Create(user);
            _unitOfWork.CommitAsync();
            return user.UserId;
        }

        public async Task CreateAccount(Guid userId)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(userId) ?? throw new ArgumentException("User was not found");
                
            
        }

        public void Delete(Guid userId)
        {
            _unitOfWork.UsersRepository.Delete(userId);
            _unitOfWork.CommitAsync();
        }

        public void Update(User user)
        {
            _unitOfWork.UsersRepository.Update(user);
            _unitOfWork.CommitAsync();
        }
    }
}