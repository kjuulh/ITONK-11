using Account.Repositories;

namespace Account.Database {
    public class UnitOfWork : IUnitOfWork {
        public IAccountRepository AccountRepository { get; }
        private readonly AccountContext _context;

        public UnitOfWork (AccountContext context, IAccountRepository accountRepository) {
            AccountRepository = accountRepository;
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