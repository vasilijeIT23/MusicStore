using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreCore.Entities;

namespace MusicStoreInfrastructure
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder) 
        {
            builder.HasKey(x => x.Id);  

            builder.Property(x => x.Quantity)
                .IsRequired();
        }

    }
}
