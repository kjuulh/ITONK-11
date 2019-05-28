using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.Models;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
    public interface ITobinTaxerService
    {
        Task<List<TaxedTransaction>> TaxTransaction(List<TransactionViewModel> transaction);
    }
}