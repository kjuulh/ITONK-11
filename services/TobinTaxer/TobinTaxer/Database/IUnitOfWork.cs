using System.Threading.Tasks;

namespace TobinTaxer.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}