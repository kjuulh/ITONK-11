using System;
using System.Threading.Tasks;
using Payment.ViewModels;

namespace Payment.Services
{
    public interface IPaymentService
    {
        Task<bool> CreateTransaction(CreateTransactionViewModel transaction);
        Task<bool> RevertTransaction(RevertTransactionViewModel transaction);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IAccountsService _accountsService;

        public PaymentService(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<bool> CreateTransaction(CreateTransactionViewModel transaction)
        {
            if (transaction.Amount <= 0.00m)
                throw new ArgumentException("Amount must be higher than 0.00");

            var buyerTransaction = await _accountsService.CreateTransaction(transaction.BuyerAccountId, -transaction.Amount);
            var sellerTransaction = await _accountsService.CreateTransaction(transaction.SellerAccountId, transaction.Amount);
            if (buyerTransaction == null || sellerTransaction == null)
            {
                if (buyerTransaction != null)
                    await _accountsService.RevertTransaction(transaction.BuyerAccountId,
                        buyerTransaction.TransactionId);
                if (sellerTransaction != null)
                    await _accountsService.RevertTransaction(transaction.SellerAccountId,
                        sellerTransaction.TransactionId);

                return false;
            }

            return true;
        }

        public async Task<bool> RevertTransaction(RevertTransactionViewModel transaction)
        {
            var buyerTransaction = await _accountsService.RevertTransaction(transaction.BuyerAccountId, transaction.BuyerTransactionId);
            var sellerTransaction = await _accountsService.RevertTransaction(transaction.SellerAccountId, transaction.SellerTransactionId);
            if (buyerTransaction == null || sellerTransaction == null)
            {
                if (buyerTransaction != null)
                    await _accountsService.RevertTransaction(transaction.BuyerAccountId,
                        buyerTransaction.TransactionId);
                if (sellerTransaction != null)
                    await _accountsService.RevertTransaction(transaction.SellerAccountId,
                        sellerTransaction.TransactionId);

                return false;
            }
            return true;
        }
    }
}