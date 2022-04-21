using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EfCore.Mapping
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(x => x.Id);

            builder.Property(i => i.Title).HasMaxLength(255).IsRequired();
            builder.Property(i => i.ShortDescription).HasMaxLength(255).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(i => i.PictureAlt).HasMaxLength(255).IsRequired();
            builder.Property(i => i.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(i => i.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(i => i.Slug).HasMaxLength(300).IsRequired();

            builder.HasOne(i=>i.ArticleCategory)
                   .WithMany(i=>i.Articles)
                   .HasForeignKey(i=>i.ArticleCategoryId);
        }
    }
}
