using System;
using System.Collections.Generic;
using Shares.Models;
using Shares.ViewModels;

namespace Shares.Services
{
    public interface ISharesService
    {
        Share Get(Guid id);
        IEnumerable<Share> GetAll();
        void Delete(Guid id);
        void Update(Share share);
        Guid Establish(ShareViewModel shareViewModel);
        Share GetByName(string name);
    }
}