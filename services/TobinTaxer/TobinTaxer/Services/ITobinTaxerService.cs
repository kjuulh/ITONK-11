using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.Models;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
    public interface ITobinTaxerService
    {
        Task<TaxedTransaction> Get(Guid id);
        Task<Guid> Register(TransactionViewModel transactionViewModel);
        IEnumerable<TaxedTransaction> GetAll();
        Task Delete(Guid id);
        TaxedTransaction Get(DateTime timestamp);
        TaxedTransaction TaxTransaction(TaxedTransaction transaction);
    }
}