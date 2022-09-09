using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardStorage.Data.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(nameof(Account.PasswordHash))
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(nameof(Account.PasswordSalt))
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
