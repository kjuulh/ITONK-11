using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
   public interface ITraderService
    {
        Task<List<TransactionViewModel>> GetTransactions(int year, int month);
    }
}
