using Bank.Repositories;

namespace Bank.Database {
    public class UnitOfWork : IUnitOfWork {
        public IBankRepository BankRepository { get; }
        private readonly BankContext _context;

        public UnitOfWork (BankContext context, IBankRepository bankRepository) {
            BankRepository = bankRepository;
            _context = context;
        }

        public void Commit () {
            _context.SaveChanges ();
        }

        public void CommitAsync () {
            _context.SaveChangesAsync ();
        }
    }
}