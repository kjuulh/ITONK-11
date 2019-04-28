using Bank.Repositories;

namespace Bank.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountsRepository AccountsRepository { get; }
        public IUsersRepository UsersRepository { get; }
        private readonly BankContext _context;

        public UnitOfWork(BankContext context, IAccountsRepository accountsRepository, IUsersRepository usersRepository)
        {
            _context = context;
            AccountsRepository = accountsRepository;
            UsersRepository = usersRepository;
        }

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