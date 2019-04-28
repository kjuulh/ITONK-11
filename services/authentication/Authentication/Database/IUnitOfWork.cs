namespace Authentication.Database {
    public interface IUnitOfWork {
        void Commit ();
        void CommitAsync ();
    }
}