using System;
using System.Collections.Generic;
using Shares.Models;

namespace Shares.Services
{
    public interface ISharesService
    {
        Share Get(Guid id);
        IEnumerable<Share> GetAll();
        void Delete(Guid id);
        void Update(Share share);
    }
}