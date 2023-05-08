using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreCore.Entities;

namespace MusicStoreInfrastructure
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Category)
                .IsRequired();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
