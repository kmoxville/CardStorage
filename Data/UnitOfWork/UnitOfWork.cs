using CardStorage.Data.Entities;
using CardStorage.Data.Repositories;

namespace CardStorage.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            Clients = new ClientRepository(context);
            Cards = new ClientCardRepository(context);

            _context = context;
        }

        public IRepository<Client> Clients { get; private set; }

        public IRepository<ClientCard> Cards { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Client>, ClientRepository>();
            services.AddScoped<IRepository<ClientCard>, ClientCardRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
