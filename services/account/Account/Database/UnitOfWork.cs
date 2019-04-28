using Account.Repositories;

namespace Account.Database {
    public class UnitOfWork : IUnitOfWork {
        public IAccountRepository AccountRepository { get; }
        public ITransactionsRepository TransactionsRepository { get; set; }
        private readonly AccountContext _context;

        public UnitOfWork (AccountContext context, IAccountRepository accountRepository, ITransactionsRepository transactionsRepository) {
            AccountRepository = accountRepository;
            TransactionsRepository = transactionsRepository;
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