using System.Threading.Tasks;

namespace Authentication.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}