using CardStorage.Data.Configurations;
using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Data
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<ClientCard> Cards { get; set; } = null!;

        public DbSet<Client> Clients { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientCardConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());

            modelBuilder.Entity<ClientCard>()
                .HasOne(cc => cc.Client)
                .WithMany(c => c.Cards)
                .HasForeignKey(cc => cc.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
