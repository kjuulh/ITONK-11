using System;
using System.Collections.Generic;
using PublicShareControl.Models;

namespace PublicShareControl.Services
{
    public interface IPortfolioService
    {
        PortfolioModel Get(Guid id);
        IEnumerable<PortfolioModel> GetAll();
        void Delete(Guid id);
        void Update(PortfolioModel model);
        Guid CreatePortfolio(PortfolioModel model);
    }
}