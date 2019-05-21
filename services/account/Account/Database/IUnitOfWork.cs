namespace Account.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
    }
}