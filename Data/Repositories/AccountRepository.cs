using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Data.Repositories
{
    public sealed class AccountRepository : IRepository<Account>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Account> _dbSet;

        public AccountRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Accounts;
        }

        public void Delete(params Account[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<Account> GetAll()
        {
            return _dbSet.AsQueryable().OrderBy(x => x.Id);
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(params Account[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(params Account[] entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
