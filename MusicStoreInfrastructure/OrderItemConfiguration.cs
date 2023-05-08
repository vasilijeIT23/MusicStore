using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreCore.Entities;

namespace MusicStoreInfrastructure
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

        }
    }
}
