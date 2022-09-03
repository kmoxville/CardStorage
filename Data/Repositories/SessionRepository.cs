using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Data.Repositories
{
    public sealed class SessionRepository : IRepository<Session>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Session> _dbSet;

        public SessionRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Sessions;
        }

        public void Delete(params Session[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<Session> GetAll()
        {
            return _dbSet.AsQueryable().OrderBy(x => x.Id).Include(x => x.Account);
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(params Session[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(params Session[] entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
