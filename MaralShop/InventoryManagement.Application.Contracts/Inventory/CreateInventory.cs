using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get; set;}
        public double UnitPrice { get; set; }
        public SelectList Products { get; set; }

    }
}
