using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PublicShareControl.Models;

namespace PublicShareControl.Repositories
{
    public interface IPortfolioRepository
    {
        PortfolioModel Get(Guid id);
        Task<PortfolioModel> GetAsync(Guid id);
        IEnumerable<PortfolioModel> GetAll();
        IAsyncEnumerable<PortfolioModel> GetAllAsync();
        void Update(PortfolioModel portfolio);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}