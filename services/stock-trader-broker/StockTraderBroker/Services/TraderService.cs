using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockTraderBroker.Database;
using StockTraderBroker.Models;
using StockTraderBroker.ViewModels;

namespace StockTraderBroker.Services
{
    public interface ITraderService
    {
        Task<Request> CreateRequest(SellShareViewModel viewModel);
        Task<Request> GetRequestByIdAsync(Guid requestId);
        IAsyncEnumerable<Request> GetAll();
        Task<Request> UpdateRequest(Guid requestId, UpdateRequestViewModel viewModel);
        Task DeleteRequest(Guid requestId);
        Task<bool> BuyShareAsync(Guid requestId, BuyShareViewModel viewModel);
    }

    public class TraderService : ITraderService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentsService _paymentsService;
        private readonly IShareControlService _shareControlService;
        private readonly ISharesService _sharesService;
        private readonly UnitOfWork _unitOfWork;

        public TraderService(IUnitOfWork unitOfWork, IAccountService accountService, IPaymentsService paymentsService,
            IShareControlService shareControlService, ISharesService sharesService)
        {
            _accountService = accountService;
            _paymentsService = paymentsService;
            _shareControlService = shareControlService;
            _sharesService = sharesService;
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public async Task<Request> CreateRequest(SellShareViewModel viewModel)
        {
            try
            {
                //TODO: Check if the user has multiple requests with the same shareId in progress
                if (!await _shareControlService.HasEnoughShares(viewModel.PortfolioId, viewModel.ShareId,
                    viewModel.Amount))
                    throw new ArgumentException("Owner porfolio doesn't have enough shares");
            }
            catch (Exception e)
            {
                throw new ArgumentException("Something went wrong, try again");
            }

            var request = new Request
            {
                RequestId = Guid.NewGuid(),
                ShareId = viewModel.ShareId,
                OwnerAccountId = viewModel.AccountId,
                PortfolioId = viewModel.PortfolioId,
                Amount = viewModel.Amount,
                Status = "open",
                DateAdded = DateTime.UtcNow
            };

            _unitOfWork.RequestsRepository.Add(request);
            await _unitOfWork.CommitAsync();

            return request;
        }

        public async Task<Request> GetRequestByIdAsync(Guid requestId)
        {
            return await _unitOfWork.RequestsRepository.GetAsync(requestId);
        }

        public IAsyncEnumerable<Request> GetAll() => _unitOfWork.RequestsRepository.GetAllAsync();

        public async Task<Request> UpdateRequest(Guid requestId, UpdateRequestViewModel viewModel)
        {
            var request = await GetRequestByIdAsync(requestId);

            if (request == null)
                return null;

            request.Amount = viewModel.Amount;

            try
            {
                //BUG: The user might have multiple request in progress at the same time
                if (!await _shareControlService.HasEnoughShares(request.PortfolioId, request.ShareId, viewModel.Amount))
                    throw new ArgumentException("Owner porfolio doesn't have enough shares");
            }
            catch (Exception e)
            {
                throw new ArgumentException("Something went wrong, try again");
            }

            _unitOfWork.RequestsRepository.Update(request);
            await _unitOfWork.CommitAsync();
            return request;
        }

        public async Task DeleteRequest(Guid requestId)
        {
            var request = await GetRequestByIdAsync(requestId);

            if (request != null)
            {
                if (request.Status != "closed")
                {
                    await _unitOfWork.RequestsRepository.DeleteAsync(requestId);
                    await _unitOfWork.CommitAsync();
                    return;
                }
            }

            throw new ArgumentException("Cannot delete a closed request");
        }

        public async Task<bool> BuyShareAsync(Guid requestId, BuyShareViewModel viewModel)
        {
            var request = await GetRequestByIdAsync(requestId);

            if (request == null)
                throw new ArgumentException("request doesn't exist");

            //TODO: Handle rollbacks
            try
            {
                // Get share from source
                var value = (await _sharesService.GetShareValue(request.ShareId)).SingleShareValue * request.Amount;

                // Check if user has enough funds
                if ((await _accountService.GetBalance(viewModel.AccountId)).Balance < value)
                    throw new ArgumentException("User doesn't have enough funds");

                // Process transaction
                if (!await _paymentsService.CreateTransaction(request.OwnerAccountId, viewModel.AccountId, value))
                    throw new ArgumentException("Couldn't create transaction");

                // Calculate taxes
                var taxes = value * 0.01m; // TAX RATE = 1%

                // Process taxes
                if (!await _accountService.PostTaxes(viewModel.AccountId, taxes))
                    throw new ArgumentException("Couldn't post taxes");

                // Move shares
                if (!await _shareControlService.ChangeOwnershipOfShare(request.ShareId, request.PortfolioId,
                    viewModel.PortfolioId, request.Amount))
                    throw new ArgumentException("Couldn't post taxes");
            }
            catch (Exception e)
            {
                throw new ArgumentException("Something went wrong, try again");
            }

            request.Status = "closed";
            request.DateClosed = DateTime.UtcNow;

            // Success, update database
            _unitOfWork.RequestsRepository.Update(request);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}