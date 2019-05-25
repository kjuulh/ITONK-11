using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Database;
using Account.Models;

namespace Account.Services
{
    public interface ITransactionsService
    {
        Task<Transaction> Get(Guid id);
        Task<Guid> AppendTransaction(Guid accountId, decimal amount);
        Task<Guid> RevertTransaction(Guid accountId, Guid transactionId);
        IEnumerable<Transaction> GetAll(Guid accountId);
    }

    public class TransactionsService : ITransactionsService
    {
        private readonly UnitOfWork _unitOfWork;

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public async Task<Transaction> Get(Guid id)
        {
            return await _unitOfWork.TransactionsRepository.GetAsync(id);
        }

        public IEnumerable<Transaction> GetAll(Guid accountId)
        {
            var account = _unitOfWork.AccountRepository.Get(accountId);

            if (account == null) throw new ArgumentException("Account was not found");

            var transactions = _unitOfWork.TransactionsRepository.GetAll(accountId);

            return transactions;
        }

        public async Task<Guid> AppendTransaction(Guid accountId, decimal amount)
        {
            var account = _unitOfWork.AccountRepository.Get(accountId);

            if (account == null) throw new ArgumentException("Account was not found");
            if (amount == 0.00m) throw new ArgumentException("Amount cannot be 0.00");

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Amount = amount,
                DateAdded = DateTime.UtcNow,
                Account = account
            };

            if (account.Transactions == null)
                account.Transactions = new List<Transaction>();

            account.Transactions.Add(transaction);
            account.Balance += transaction.Amount;

            if (account.Balance >= 0.00m)
            {
                _unitOfWork.TransactionsRepository.Create(transaction);
                _unitOfWork.AccountRepository.Update(account);
                await _unitOfWork.CommitAsync();
                return transaction.TransactionId;
            }

            return Guid.Empty;
        }

        public async Task<Guid> RevertTransaction(Guid accountId, Guid transactionId)
        {
            var account = _unitOfWork.AccountRepository.Get(accountId);
            if (account == null) throw new ArgumentException("Account was not found");
            var transaction = await _unitOfWork.TransactionsRepository.Get(transactionId);
            if (transaction == null) throw new ArgumentException("Transaction was not found");

            var revertedTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Amount = transaction.Amount * -1,
                DateAdded = DateTime.UtcNow,
                Account = account
            };

            account.Transactions.Add(revertedTransaction);
            account.Balance += revertedTransaction.Amount;

            _unitOfWork.TransactionsRepository.Create(revertedTransaction);
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.CommitAsync();
            return revertedTransaction.TransactionId;
        }
    }
}