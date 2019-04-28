namespace PublicShareControl.Database
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
    }
}