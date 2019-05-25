using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shares.Database;
using Shares.Models;
using Shares.ViewModels;

namespace Shares.Services
{
    public interface ISharesService
    {
        Share Get(Guid id);
        IEnumerable<Share> GetAll();
        Task Delete(Guid id);
        Task Update(ChangeShareViewModel share);
        Task<Guid> Establish(ShareViewModel shareViewModel);
        Share GetByName(string name);
    }

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

        public async Task Delete(Guid id)
        {
            _unitOfWork.SharesRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(ChangeShareViewModel share)
        {
            var shareToUpdate = _unitOfWork.SharesRepository.Get(share.ShareId);

            if (share.Name != null)
                shareToUpdate.Name = share.Name;
            if (share.TotalCount != default(int))
                shareToUpdate.TotalCount = share.TotalCount;
            if (share.TotalValue != default(decimal))
                shareToUpdate.TotalValue = share.TotalValue;
            shareToUpdate.SingleShareValue = GetSingleShareValue(shareToUpdate.TotalValue, shareToUpdate.TotalCount);

            _unitOfWork.SharesRepository.Update(shareToUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Guid> Establish(ShareViewModel shareViewModel)
        {
            var share = new Share
            {
                Name = shareViewModel.Name.ToLower(),
                ShareId = new Guid(),
                SingleShareValue = GetSingleShareValue(shareViewModel.TotalValue, shareViewModel.TotalCount),
                TotalCount = shareViewModel.TotalCount,
                TotalValue = shareViewModel.TotalValue
            };
            _unitOfWork.SharesRepository.Establish(share);
            await _unitOfWork.CommitAsync();
            return share.ShareId;
        }

        public Share GetByName(string name)
        {
            return _unitOfWork.SharesRepository.GetByName(name.ToLower());
        }

        #region helper

        private static decimal GetSingleShareValue(decimal totalValue, int count)
        {
            return totalValue / count;
        }

        #endregion
    }
}