using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Data.Repositories
{
    public sealed class ClientCardRepository : IRepository<ClientCard>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<ClientCard> _dbSet;

        public ClientCardRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Cards;
        }

        public void Delete(params ClientCard[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<ClientCard> GetAll()
        {
            return _dbSet.AsQueryable().OrderBy(x => x.Id).Include(x => x.Client);
        }

        public async Task<ClientCard?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(params ClientCard[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(params ClientCard[] entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
