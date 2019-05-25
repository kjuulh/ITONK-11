using System.Threading.Tasks;
using StockTraderBroker.Repositories;

namespace StockTraderBroker.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockTraderBrokerContext _context;

        public UnitOfWork(StockTraderBrokerContext context, IAccountsRepository accountsRepository, IUsersRepository usersRepository)
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