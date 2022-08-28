using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Data.Repositories
{
    public sealed class ClientRepository : IRepository<Client>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Client> _dbSet;

        public ClientRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Clients;
        }

        public void Delete(params Client[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<Client> GetAll()
        {
            return _dbSet.AsQueryable().OrderBy(x => x.Id);
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(params Client[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(params Client[] entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
