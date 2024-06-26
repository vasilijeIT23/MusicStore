﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreCore.Entities;

namespace MusicStoreInfrastructure
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(c => c.Status)
                .IsRequired();
            builder.Property(c => c.Role)
                .IsRequired();
            builder.Property(c => c.Username)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(c => c.Password)
                .IsRequired();
            builder.Property(c => c.Salt)
                .IsRequired();

            builder.HasMany(c => c.Orders)
                .WithOne(c => c.Customer);

            builder.HasMany(c => c.Reviews)
                .WithOne(c => c.Customer);

        }
    }
}
