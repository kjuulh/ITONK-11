using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.Models;

namespace TobinTaxer.Repositories
{
    public interface ITobinTaxerRepository
    {
        TaxedTransaction Get(Guid id);
        Task<TaxedTransaction> GetAsync(Guid id);
        IEnumerable<TaxedTransaction> GetAll();
        IAsyncEnumerable<TaxedTransaction> GetAllAsync();
        void Register(TaxedTransaction transaction);
        void Update(TaxedTransaction transaction);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        Task<TaxedTransaction> GetAsync(DateTime timestamp);
    }
}