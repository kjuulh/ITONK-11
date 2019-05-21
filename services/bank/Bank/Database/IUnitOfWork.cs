using System.Threading.Tasks;

namespace Bank.Database {
    public interface IUnitOfWork {
        void Commit ();
        Task CommitAsync ();
    }
}