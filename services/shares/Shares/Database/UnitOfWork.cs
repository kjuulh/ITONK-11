using Shares.Repositories;

namespace Shares.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISharesRepository SharesRepository { get; }
        private readonly SharesContext _context;

        public UnitOfWork(SharesContext context, ISharesRepository sharesRepository)
        {
            SharesRepository = sharesRepository;
            _context = context;
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