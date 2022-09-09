using CardStorage.Data.Entities;

namespace CardStorage.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Client> Clients { get; }
        IRepository<ClientCard> Cards { get; }
        IRepository<Account> Accounts { get; }
        IRepository<Session> Sessions { get; }

        Task SaveAsync();
    }
}
