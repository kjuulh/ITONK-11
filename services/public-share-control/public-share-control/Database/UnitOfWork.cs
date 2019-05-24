using PublicShareControl.Repositories;

namespace PublicShareControl.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PSCContext _context;

        public UnitOfWork(PSCContext context, IPortfolioRepository portfolioRepository)
        {
            PortfolioRepository = portfolioRepository;
            _context = context;
        }

        public IPortfolioRepository PortfolioRepository { get; }

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