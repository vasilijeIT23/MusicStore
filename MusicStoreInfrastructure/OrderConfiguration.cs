using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreCore.Entities;

namespace MusicStoreInfrastructure
{
    public class OrderConfiguration :IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .IsRequired();
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order);
        }
    }
}
