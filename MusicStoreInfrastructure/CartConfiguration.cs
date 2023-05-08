using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreCore.Entities;

namespace MusicStoreInfrastructure
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder) 
        {
            builder.HasKey(x=> x.Id);

            builder.HasMany(x => x.CartItems)
                .WithOne(x => x.Cart);
        }
    }
}
