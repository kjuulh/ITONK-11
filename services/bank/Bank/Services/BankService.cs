using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Database;
using Bank.Models;

namespace Bank.Services
{
    public interface IBankService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUser(Guid userId);
        Task<Guid> ReconcileUser(Guid userId);
        Task<User> CreateAccount(Guid userId);
        Task Delete(Guid userId);
        Task Update(User user);
    }

    public class BankService : IBankService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IUsersService _usersService;
        private readonly IAccountService _accountService;

        public BankService(IUnitOfWork unitOfWork, IUsersService usersService, IAccountService accountsService)
        {
            _usersService = usersService;
            _accountService = accountsService;
            _unitOfWork = (UnitOfWork) unitOfWork;
        }
        
        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _unitOfWork.UsersRepository.GetAllAsync().ToList();

            foreach (var user in users)
            {
                user.Accounts = _unitOfWork.AccountsRepository.GetByUserIdAsync(user.UserId).ToList();
            }

            return users;
        }

        public async Task<User> GetUser(Guid userId)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(userId);

            user.Accounts = _unitOfWork.AccountsRepository.GetByUserIdAsync(user.UserId).ToList();

            return user;
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

            var localUser = await _unitOfWork.UsersRepository.GetAsync(userId);
            
            if (localUser == null)
            {
                _unitOfWork.UsersRepository.Create(user);
                await _unitOfWork.CommitAsync();
                return user.UserId;
            }
            return localUser.UserId;
        }

        public async Task<User> CreateAccount(Guid userId)
        {
            var userIdSoT = await ReconcileUser(userId); // User id source of truth
            if (userIdSoT == Guid.Empty) throw new ArgumentException("User was not found");
            
            var user = await _unitOfWork.UsersRepository.GetAsync(userId) ?? throw new ArgumentException("User was not found");

            var account = await _accountService.CreateAccount();
            if (account == null) throw new Exception("Account couldn't be created");

            var accountRelation = new Account()
            {
                AccountId = account.AccountId,
                User = user
            };
            
            _unitOfWork.AccountsRepository.Create(accountRelation);
            await _unitOfWork.CommitAsync();
            
            user.Accounts.Add(accountRelation);
            await _unitOfWork.CommitAsync();

            return await GetUser(user.UserId);
        }

        public async Task Delete(Guid userId)
        {
            _unitOfWork.UsersRepository.Delete(userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(User user)
        {
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.CommitAsync();
        }
    }
}