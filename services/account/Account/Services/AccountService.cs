using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Database;

namespace Account.Services
{
    public interface IAccountService
    {
        Task<Models.Account> Get(Guid id);
        Task<Guid> Create();
        IEnumerable<Models.Account> GetAll();
        Task Delete(Guid id);
        Task Update(Models.Account account);
    }

    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public async Task<Models.Account> Get(Guid id)
        {
            return await _unitOfWork.AccountRepository.GetAsync(id);
        }

        public async Task<Guid> Create()
        {
            var account = new Models.Account
            {
                AccountId = Guid.NewGuid(),
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.AccountRepository.Register(account);
            await _unitOfWork.CommitAsync();
            return account.AccountId;
        }

        public IEnumerable<Models.Account> GetAll()
        {
            return _unitOfWork.AccountRepository.GetAllAsync().ToEnumerable();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.AccountRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(Models.Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.CommitAsync();
        }
    }
}