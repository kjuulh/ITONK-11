using System.Threading.Tasks;
using Bank.Repositories;

namespace Bank.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankContext _context;

        public UnitOfWork(BankContext context, IAccountsRepository accountsRepository, IUsersRepository usersRepository)
        {
            _context = context;
            AccountsRepository = accountsRepository;
            UsersRepository = usersRepository;
        }

        public IAccountsRepository AccountsRepository { get; }
        public IUsersRepository UsersRepository { get; }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}