using Account.Repositories;

namespace Account.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountContext _context;

        public UnitOfWork(AccountContext context, IAccountRepository accountRepository,
            ITransactionsRepository transactionsRepository)
        {
            AccountRepository = accountRepository;
            TransactionsRepository = transactionsRepository;
            _context = context;
        }

        public IAccountRepository AccountRepository { get; }
        public ITransactionsRepository TransactionsRepository { get; set; }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void CommitAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}