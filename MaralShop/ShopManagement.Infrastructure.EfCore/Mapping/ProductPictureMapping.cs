using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(i => i.PictureAlt).HasMaxLength(255).IsRequired();
            builder.Property(i => i.PictureTitle).HasMaxLength(500).IsRequired();

            builder.HasOne(i => i.Product)
                .WithMany(i => i.ProductPictures)
                .HasForeignKey(i => i.ProductId);
        }
    }
}
