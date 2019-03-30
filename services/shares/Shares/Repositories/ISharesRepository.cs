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
        IEnumerable<Share> GetAll();
        IAsyncEnumerable<Share> GetAllAsync();
        void Register(Share share);
        void Update(Share share);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}