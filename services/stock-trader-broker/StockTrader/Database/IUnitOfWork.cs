namespace StockTrader.Database {
    public interface IUnitOfWork {
        void Commit ();
        void CommitAsync ();
    }
}