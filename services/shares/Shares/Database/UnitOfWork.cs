using System.Threading.Tasks;
using Shares.Repositories;

namespace Shares.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SharesContext _context;

        public UnitOfWork(SharesContext context, ISharesRepository sharesRepository)
        {
            SharesRepository = sharesRepository;
            _context = context;
        }

        public ISharesRepository SharesRepository { get; }

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