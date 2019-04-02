using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shares.Models;

namespace Shares.Repositories
{
    public interface ISharesRepository
    {
        Share Get(Guid id);
        Task<Share> GetAsync(Guid id);
        Share GetByName(string name);
        Task<Share> GetByNameAsync(string name);
        IEnumerable<Share> GetAll();
        IAsyncEnumerable<Share> GetAllAsync();
        void Establish(Share share);
        void Update(Share share);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}