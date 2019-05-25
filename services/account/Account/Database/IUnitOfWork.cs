using System.Threading.Tasks;

namespace Account.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}