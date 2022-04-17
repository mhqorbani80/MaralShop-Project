using _0_Framework.Application;
using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrastructure.EfCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventories.Select(i=>new EditInventory { 
                Id=i.Id,
                ProductId=i.ProductId,
                UnitPrice=i.UnitPrice,
            }).FirstOrDefault(i => i.Id == id);
        }

        public List<InventoryOperationModel> GetOperation(long inventoryId)
        {
            var inventory = _inventoryContext.Inventories.FirstOrDefault(i => i.Id == inventoryId);
           return inventory.InventoryOperations.Select(i=>new InventoryOperationModel
            {
                Id=i.Id,
                Count=i.Count,
                Description=i.Description,
                OperatorId=i.OperatorId,
                OrderId=i.OrderId,
                Operator="مدیر سیستم",
                Operation=i.Operation,
                CurrentCount=i.CurrentCount
            }).ToList();
        }

        public Inventory GetProduct(long productId)
        {
            return _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryViewModel> Search(InvnetorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(i => new { i.Id, i.Name }).ToList();
            var query = _inventoryContext.Inventories.Select(i => new InventoryViewModel
            {
                Id = i.Id,
                UnitPrice=i.UnitPrice,
                ProductId=i.ProductId,
                InStock=i.InStock,
                CurrentCount=i.CalculateCurrentCount(),
                CreationDate=i.CreationDate.ToFarsi()
            });
            if(searchModel.ProductId > 0)
            {
                query= query.Where(i=>i.ProductId==searchModel.ProductId);
            }
            if (searchModel.InStock)
            {
                query= query.Where(i=> !i.InStock);
            }

            var inventories = query.OrderByDescending(i => i.Id).ToList();
            inventories.ForEach(inventory =>
                inventory.Product = products.FirstOrDefault(product => product.Id == inventory.ProductId)?.Name);

            return inventories;
        }
    }
}
