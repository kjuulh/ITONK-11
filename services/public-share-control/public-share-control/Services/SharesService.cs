using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PublicShareControl.Database;
using PublicShareControl.Models;
using PublicShareControl.ViewModels;

namespace PublicShareControl.Services
{
    public interface ISharesService
    {
        Task<Share> GetByPortfolio(Guid portfolioId, Guid shareId);
        IAsyncEnumerable<Share> GetAllByPortfolio(Guid portfolioId);
        IAsyncEnumerable<Portfolio> GetAll();

        /// <summary>
        /// Should only be used on creating shares, not when transfering them
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <param name="sharesViewModel"></param>
        /// <returns></returns>
        Task<Share> Create(Guid portfolioId, CreateSharesViewModel sharesViewModel);
        Task ChangeOwnership(ChangeOwnershipViewModel changeOwnershipViewModel);
    }

    public class SharesService : ISharesService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;


        public SharesService(IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork)
        {
            _httpClientFactory = httpClientFactory;
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public Task<Share> GetByPortfolio(Guid portfolioId, Guid shareId)
        {
            return _unitOfWork.SharesRepository.GetShareByPortfolioAsync(portfolioId, shareId);
        }

        public IAsyncEnumerable<Share> GetAllByPortfolio(Guid portfolioId)
        {
            return _unitOfWork.SharesRepository.GetAllByPortfolio(portfolioId);
        }

        public IAsyncEnumerable<Portfolio> GetAll()
        {
            return _unitOfWork.PortfolioRepository.GetAllAsync();
        }

        /// <summary>
        /// Should only be used on creating shares, not when transfering them
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <param name="sharesViewModel"></param>
        /// <returns></returns>
        public async Task<Share> Create(Guid portfolioId, CreateSharesViewModel sharesViewModel)
        {
            Portfolio portfolio = await _unitOfWork.PortfolioRepository.GetAsync(portfolioId);
            if (portfolio == null) throw new ArgumentException("Portfolio not found");

            Share share;
            try
            {
                var shareSot = await CreateShare(sharesViewModel.Name, sharesViewModel.TotalCount, sharesViewModel.TotalValue);
                share = new Share
                {
                    Portfolio = portfolio,
                    ShareId = shareSot.ShareId,
                    Count = shareSot.TotalCount
                };
                portfolio.Shares.Add(share);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Share couldn't be created");
            }
            
            _unitOfWork.SharesRepository.Add(share);
            _unitOfWork.PortfolioRepository.Update(portfolio);
            await _unitOfWork.CommitAsync();
            return share;
        }

        public async Task ChangeOwnership(ChangeOwnershipViewModel changeOwnershipViewModel)
        {
            Portfolio ownersPortfolio = await _unitOfWork.PortfolioRepository.GetAsync(changeOwnershipViewModel.OwnerPortfolioId);
            Portfolio receiverPortfolio = await _unitOfWork.PortfolioRepository.GetAsync(changeOwnershipViewModel.ReceiverPortfolioId);
            Share ownersShare =
                await _unitOfWork.SharesRepository.GetShareByPortfolioAsync(ownersPortfolio.PortfolioId,
                    changeOwnershipViewModel.ShareId);
            
            if (ownersPortfolio == null || receiverPortfolio == null || ownersShare == null)
                throw new ArgumentException("Id's doesn't match");
            
            // Handling owners shares
            if (changeOwnershipViewModel.Count < ownersShare.Count)
            {
                ownersShare.Count -= changeOwnershipViewModel.Count;
                _unitOfWork.SharesRepository.Update(ownersShare);
            }
            else if (changeOwnershipViewModel.Count == ownersShare.Count)
            {
                await _unitOfWork.SharesRepository.DeleteShareByPortfolioAsync(ownersPortfolio.PortfolioId,
                    ownersShare.ShareId);
            }
            else
            {
                throw new Exception("Owner doesn't have enough shares");
            }

            // Handling receivers shares
            Share receiversShare =
                receiverPortfolio.Shares.SingleOrDefault(share => share.ShareId == changeOwnershipViewModel.ShareId);
            if (receiversShare == null)
            {
                receiversShare = new Share
                {
                    Portfolio = receiverPortfolio,
                    ShareId = ownersShare.ShareId,
                    Count = changeOwnershipViewModel.Count
                };
                receiverPortfolio.Shares.Add(receiversShare);
                _unitOfWork.SharesRepository.Add(receiversShare);
                _unitOfWork.PortfolioRepository.Update(receiverPortfolio);
            }
            else
            {
                receiversShare.Count += changeOwnershipViewModel.Count;
                _unitOfWork.SharesRepository.Update(receiversShare);
            }
            
            // Commiting
            await _unitOfWork.CommitAsync();
        }

        private async Task<SharesViewModel> CreateShare(string name, int count, decimal value)
        {
            var sharesServiceDNS = Environment.GetEnvironmentVariable("SHARES_SERVICE_DNS");
            if (string.IsNullOrEmpty(sharesServiceDNS))
                throw new NullReferenceException("SHARES_SERVICE_DNS url is null");
            var sharesServicePORT = Environment.GetEnvironmentVariable("SHARES_SERVICE_PORT");
            if (string.IsNullOrEmpty(sharesServicePORT))
                throw new NullReferenceException("SHARES_SERVICE_PORT url is null");
            var requestUri = "http://" + sharesServiceDNS + ":" + sharesServicePORT + "/api/Shares/";
            var request = HttpRequestPost(requestUri,
                new
                {
                    Name = name,
                    TotalCount = count,
                    TotalValue = value
                },
                out var client);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SharesViewModel>();
            }

            return null;
        }

        public class SharesViewModel
        {
            public Guid ShareId { get; set; }
            public string Name { get; set; }
            public decimal TotalValue { get; set; }
            public int TotalCount { get; set; }
            public decimal SingleShareValue { get; set; }
        }

        private HttpRequestMessage HttpRequestGet(string requestUri, out HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                requestUri);
            request.Headers.Add("Accept", "application/json");

            client = _httpClientFactory.CreateClient();
            return request;
        }

        private HttpRequestMessage HttpRequestPost(string requestUri, object content, out HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                requestUri);
            request.Headers.Add("Accept", "application/json");

            var jsonContent = JsonConvert.SerializeObject(content, Formatting.None).ToLower();

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            client = _httpClientFactory.CreateClient();
            return request;
        }
    }
}