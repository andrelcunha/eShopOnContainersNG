
using ALC.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ALC.Catalog.API.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(keyExpression: c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.ImageUrl)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.ToTable("Products");
    }
}
