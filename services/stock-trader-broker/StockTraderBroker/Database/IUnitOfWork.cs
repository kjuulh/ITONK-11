using System.Threading.Tasks;

namespace StockTraderBroker.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}