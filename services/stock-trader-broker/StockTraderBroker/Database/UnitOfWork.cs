using System.Threading.Tasks;
using StockTraderBroker.Repositories;

namespace StockTraderBroker.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }


    public class UnitOfWork : IUnitOfWork
    {
        public IRequestsRepository RequestsRepository { get; set; }
        private readonly StockTraderBrokerContext _context;

        public UnitOfWork(StockTraderBrokerContext context, IRequestsRepository requestsRepository)
        {
            this._context = context;
            this.RequestsRepository = requestsRepository;
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}