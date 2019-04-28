namespace PublicShareControl.Database
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly PSCContext _context;
        
        
        public UnitOfWork(PSCContext context)
        {
            
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