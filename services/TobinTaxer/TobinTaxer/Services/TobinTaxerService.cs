using System.Collections.Generic;
using System.Threading.Tasks;
using TobinTaxer.Models;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Services
{
    public class TobinTaxerService : ITobinTaxerService
    {
        private ISharesService _shareService;
        

        public TobinTaxerService(ISharesService shareService)
        {
            _shareService = shareService;

        }


        public async Task<List<TaxedTransaction>> TaxTransaction(List<TransactionViewModel> transactionViewmodels)
        {
            List<TaxedTransaction> taxedTransactions = new List<TaxedTransaction>();
            //Tax transactions
            foreach (var transaction in transactionViewmodels)
            {
                var taxedTransaction = new TaxedTransaction();
                var shareValue = await _shareService.GetShareValue(transaction.ShareId);
                taxedTransaction.Value = shareValue.SingleShareValue * transaction.Amount;
                taxedTransaction.TaxedValue = taxedTransaction.Value * 0.02m;
                taxedTransaction.DateTaxed = transaction.DateClosed;
                taxedTransaction.Taxed = true;
                taxedTransaction.OwnerId = transaction.OwnerAccountId;

                taxedTransactions.Add(taxedTransaction);
            }
            

            return taxedTransactions;
        }
    }
}