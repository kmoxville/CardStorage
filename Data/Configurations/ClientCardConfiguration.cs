using CardStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardStorage.Data.Configurations
{
    internal sealed class ClientCardConfiguration : IEntityTypeConfiguration<ClientCard>
    {
        public void Configure(EntityTypeBuilder<ClientCard> builder)
        {
            builder.Property(nameof(ClientCard.Number))
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(nameof(ClientCard.CVV))
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
