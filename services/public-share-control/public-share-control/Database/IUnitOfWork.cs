using System.Threading.Tasks;

namespace PublicShareControl.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}