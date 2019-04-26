namespace Shares.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
    }
}