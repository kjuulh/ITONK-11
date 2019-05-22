using System.Threading.Tasks;
using TobinTaxer.Repositories;

namespace TobinTaxer.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITobinTaxerRepository TobinTaxerRepository { get; }
        private readonly TobinTaxerContext _context;

        public UnitOfWork(TobinTaxerContext context, ITobinTaxerRepository tobinTaxerRepository)
        {
            TobinTaxerRepository = TobinTaxerRepository;
            _context = context;
        }

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