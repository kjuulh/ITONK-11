using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockTraderBroker.Database;
using StockTraderBroker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace StockTraderBroker.Repositories
{
    public interface IRequestsRepository
    {
        Request Get(Guid requestId);
        Task<Request> GetAsync(Guid requestId);
        IEnumerable<Request> GetAll();
        IAsyncEnumerable<Request> GetAllAsync();
        IAsyncEnumerable<Request> GetByUserId(Guid userId);
        IQueryable<Request> GetByUserIdAsync(Guid userId);
        IAsyncEnumerable<Request> GetByShareId(Guid shareId);
        IQueryable<Request> GetByShareIdAsync(Guid shareId);
        void Add(Request request);
        void Update(Request request);
        void Delete(Guid requestId);
        Task DeleteAsync(Guid id);
    }

    public class RequestsRepository : IRequestsRepository
    {
        private readonly DbSet<Request> _requestsEntity;
        private readonly StockTraderBrokerContext _context;

        public RequestsRepository(StockTraderBrokerContext context)
        {
            _context = context;
            _requestsEntity = context.Set<Request>();
        }

        public void Add(Request request)
        {
            _context.Entry(request).State = EntityState.Added;
        }

        public void Delete(Guid requestId)
        {
            var requestToDelete = Get(requestId);
            if (requestToDelete != null) _requestsEntity.Remove(requestToDelete);
        }

        public async Task DeleteAsync(Guid requestId)
        {
            var requestToDelete = await GetAsync(requestId);
            if (requestToDelete != null) _requestsEntity.Remove(requestToDelete);
        }

        public Request Get(Guid requestId)
        {
            return GetAsync(requestId).Result;
        }

        public async Task<Request> GetAsync(Guid requestId)
        {
            return await _requestsEntity.SingleOrDefaultAsync(request => request.RequestId == requestId);
        }

        public IEnumerable<Request> GetAll()
        {
            return GetAllAsync().ToEnumerable();
        }

        public IAsyncEnumerable<Request> GetAllAsync()
        {
            return _requestsEntity.ToAsyncEnumerable();
        }

        public IAsyncEnumerable<Request> GetByShareId(Guid shareId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Request> GetByShareIdAsync(Guid shareId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Request> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Request> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Request request)
        {
            _context.Entry(request).State = EntityState.Modified;
        }
    }
}