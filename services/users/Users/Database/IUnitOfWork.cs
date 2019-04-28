using System.Threading.Tasks;

namespace Users.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}