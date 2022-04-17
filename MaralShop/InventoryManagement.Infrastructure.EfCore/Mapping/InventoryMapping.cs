using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EfCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");
            builder.HasKey(x => x.Id);

            builder.OwnsMany(i => i.InventoryOperations, modelBuilder =>
              {
                  modelBuilder.ToTable("InventoryOperations");
                  modelBuilder.HasKey(x => x.Id);

                  modelBuilder.Property(i => i.Description).HasMaxLength(1000);
                  modelBuilder.WithOwner(i=>i.Inventory)
                              .HasForeignKey(i => i.InventoryId);
              });
        }
    }
}