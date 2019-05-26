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

        public async Task<TaxedTransaction> Get(Guid id)
        {
            return await _unitOfWork.TobinTaxerRepository.GetAsync(id);
        }

        public async Task<Guid> Register(TaxedTransactionViewModel transactionViewModel)
        {
            var transaction = new TaxedTransaction
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

        public IEnumerable<TaxedTransaction> GetAll()
        {
            return _unitOfWork.TobinTaxerRepository.GetAllAsync().ToEnumerable();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.TobinTaxerRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public TaxedTransaction Get(DateTime timestamp)
        {
            return _unitOfWork.TobinTaxerRepository.GetAsync(timestamp).Result;
        }

        public TaxedTransaction TaxTransaction(TransactionViewModel transaction)
        {
            TaxedTransaction taxedTransaction = new TaxedTransaction();
            //Tax transactions

            taxedTransaction.TaxedValue = transaction.Value - (transaction.Value / 2 * 100);
            taxedTransaction.DateTaxed = DateTime.Now;
            taxedTransaction.Taxed = true;

            return taxedTransaction;
        }
    }
}