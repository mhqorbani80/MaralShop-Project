using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");

            builder.HasKey(x => x.Id);

            builder.Property(i => i.Description).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(i => i.PictureAlt).HasMaxLength(255).IsRequired();
            builder.Property(i => i.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Title).HasMaxLength(250).IsRequired();
            builder.Property(i => i.Heading).HasMaxLength(250).IsRequired();
            builder.Property(i => i.BtnText).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Link).HasMaxLength(300).IsRequired();
        }
    }
}
