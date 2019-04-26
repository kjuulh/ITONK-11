using System.Threading.Tasks;
using Users.Repositories;

namespace Users.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository UsersRepository { get; }
        private readonly UsersContext _context;

        public UnitOfWork(UsersContext context, IUsersRepository usersRepository)
        {
            UsersRepository = usersRepository;
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