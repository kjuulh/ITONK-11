using System.Threading.Tasks;
using PublicShareControl.Repositories;

namespace PublicShareControl.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PSCContext _context;

        public UnitOfWork(PSCContext context, IPortfolioRepository portfolioRepository, ISharesRepository sharesRepository)
        {
            PortfolioRepository = portfolioRepository;
            SharesRepository = sharesRepository;
            _context = context;
        }

        public IPortfolioRepository PortfolioRepository { get; }
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