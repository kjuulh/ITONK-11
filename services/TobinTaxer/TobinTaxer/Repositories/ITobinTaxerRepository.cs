using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.Models;

namespace TobinTaxer.Repositories
{
    public interface ITobinTaxerRepository
    {
        Transaction Get(Guid id);
        Task<Transaction> GetAsync(Guid id);
        IEnumerable<Transaction> GetAll();
        IAsyncEnumerable<Transaction> GetAllAsync();
        void Register(Transaction user);
        void Update(Transaction user);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        Task<Transaction> GetAsync(DateTime timestamp);
    }
}