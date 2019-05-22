using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TobinTaxer.Database;
using TobinTaxer.Models;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
    public class TobinTaxerService : ITobinTaxerService
    {
        private readonly UnitOfWork _unitOfWork;

        public TobinTaxerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public async Task<Transaction> Get(Guid id)
        {
            return await _unitOfWork.TobinTaxerRepository.GetAsync(id);
        }

        public async Task<Guid> Register(TransactionViewModel transactionViewModel)
        {
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Value = transactionViewModel.Value,
                TaxedValue = transactionViewModel.TaxedValue,
                DateTaxed = DateTime.UtcNow
            };

            _unitOfWork.TobinTaxerRepository.Register(transaction);
            await _unitOfWork.CommitAsync();
            return transaction.TransactionId;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _unitOfWork.TobinTaxerRepository.GetAllAsync().ToEnumerable();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.TobinTaxerRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public Transaction Get(DateTime timestamp)
        {
            return _unitOfWork.TobinTaxerRepository.GetAsync(timestamp).Result;
        }
    }
}