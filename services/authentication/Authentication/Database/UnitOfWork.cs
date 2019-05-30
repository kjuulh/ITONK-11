using System.Threading.Tasks;
using Authentication.Repositories;

namespace Authentication.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthenticationRepository AuthenticationRepository { get; }
        private readonly AuthenticationContext _context;

        public UnitOfWork(AuthenticationContext context, IAuthenticationRepository authenticationRepository)
        {
            AuthenticationRepository = authenticationRepository;
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