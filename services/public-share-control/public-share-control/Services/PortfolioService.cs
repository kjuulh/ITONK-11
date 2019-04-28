using System;
using System.Collections.Generic;
using PublicShareControl.Database;
using PublicShareControl.Models;

namespace PublicShareControl.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly UnitOfWork _unitOfWork;

        public PortfolioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public PortfolioModel Get(Guid id)
        {
            return _unitOfWork.PortfolioRepository.Get(id);
        }

        public IEnumerable<PortfolioModel> GetAll()
        {
            return _unitOfWork.PortfolioRepository.GetAll();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.PortfolioRepository.Delete(id);
        }

        public void Update(PortfolioModel model)
        {
            _unitOfWork.PortfolioRepository.Update(model);
        }

        public Guid CreatePortfolio(PortfolioModel model)
        {
            var portfolio = new PortfolioModel()
            {
                Owner = Guid.NewGuid(),
                Shares = null
            };
            _unitOfWork.PortfolioRepository.CreatePortfolio(portfolio);
            _unitOfWork.CommitAsync();
            return portfolio.Id;
        }
    }
}