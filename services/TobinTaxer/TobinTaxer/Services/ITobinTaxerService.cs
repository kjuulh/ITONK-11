using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.Models;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
    public interface ITobinTaxerService
    {
        Task<Transaction> Get(Guid id);
        Task<Guid> Register(TransactionViewModel transactionViewModel);
        IEnumerable<Transaction> GetAll();
        Task Delete(Guid id);
        Transaction Get(DateTime timestamp);
    }
}