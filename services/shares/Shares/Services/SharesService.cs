using System;
using System.Collections.Generic;
using System.Linq;
using Shares.Database;
using Shares.Models;
using Shares.Repositories;
using Shares.ViewModels;

namespace Shares.Services
{
    public class SharesService : ISharesService
    {
        private readonly UnitOfWork _unitOfWork;

        public SharesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public Share Get(Guid id)
        {
            return _unitOfWork.SharesRepository.GetAsync(id).Result;
        }

        public IEnumerable<Share> GetAll()
        {
            return _unitOfWork.SharesRepository.GetAllAsync().ToEnumerable();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.SharesRepository.Delete(id);
            _unitOfWork.CommitAsync();
        }

        public void Update(Share share)
        {
            share.Name = share.Name.ToLower();
            _unitOfWork.SharesRepository.Update(share);
            _unitOfWork.CommitAsync();
        }

        public Guid Establish(ShareViewModel shareViewModel)
        {
            var share = new Share()
            {
                Name = shareViewModel.Name.ToLower(),
                ShareId = new Guid(),
                SingleShareValue = shareViewModel.TotalValue/shareViewModel.TotalCount,
                TotalCount = shareViewModel.TotalCount,
                TotalValue = shareViewModel.TotalValue
            };
            _unitOfWork.SharesRepository.Establish(share);
            _unitOfWork.CommitAsync();
            return share.ShareId;
        }

        public Share GetByName(string name)
        {
            return _unitOfWork.SharesRepository.GetByName(name.ToLower());
        }
    }
}