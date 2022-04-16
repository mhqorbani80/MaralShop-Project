using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Application.Implementation;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.EfCore;
using InventoryManagement.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddDbContext<InventoryContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}