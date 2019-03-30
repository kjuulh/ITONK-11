namespace Users.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
    }
}