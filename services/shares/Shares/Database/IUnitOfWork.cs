using System.Threading.Tasks;

namespace Shares.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}