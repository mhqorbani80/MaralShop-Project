using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EfCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(i => i.Name).HasMaxLength(200);
            builder.Property(i => i.Email).HasMaxLength(500);
            builder.Property(i => i.Message).HasMaxLength(1000);

            builder.HasOne(i => i.Parent)
                   .WithMany(i => i.Children)
                   .HasForeignKey(i => i.ParentId);
        }
    }
}