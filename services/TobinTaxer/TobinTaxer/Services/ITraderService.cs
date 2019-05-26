using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
   public interface ITraderService
    {
        Task<TransactionViewModel> GetTransaction(DateTime dateAdded);
    }
}
