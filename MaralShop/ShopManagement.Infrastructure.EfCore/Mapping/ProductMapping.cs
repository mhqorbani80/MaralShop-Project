using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).HasMaxLength(255).IsRequired();
            builder.Property(i => i.Code).HasMaxLength(20).IsRequired();
            builder.Property(i => i.ShortDescription).HasMaxLength(255).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(i => i.PictureAlt).HasMaxLength(255).IsRequired();
            builder.Property(i => i.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(i => i.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(i => i.Slug).HasMaxLength(300).IsRequired();

            builder.HasOne(i=>i.ProductCategory)
                .WithMany(i=>i.Products)
                .HasForeignKey(i=>i.ProductCategoryId);

            builder.HasMany(i=>i.ProductPictures)
                .WithOne(i=>i.Product)
                .HasForeignKey(i=>i.ProductId);

        }
    }
}
