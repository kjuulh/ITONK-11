using StockTrader.Repositories;

namespace StockTrader.Database {
    public class UnitOfWork : IUnitOfWork {
        private readonly StockTraderContext _context;

        public UnitOfWork (StockTraderContext context, IStockTraderRepository stockTraderRepository,
            ITransactionsRepository transactionsRepository) {
            StockTraderRepository = stockTraderRepository;
            TransactionsRepository = transactionsRepository;
            _context = context;
        }

        public IStockTraderRepository StockTraderRepository { get; }
        public ITransactionsRepository TransactionsRepository { get; set; }

        public void Commit () {
            _context.SaveChanges ();
        }

        public void CommitAsync () {
            _context.SaveChangesAsync ();
        }
    }
}