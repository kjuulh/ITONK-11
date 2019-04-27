namespace Bank.Database {
    public interface IUnitOfWork {
        void Commit ();
        void CommitAsync ();
    }
}