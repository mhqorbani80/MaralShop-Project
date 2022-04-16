﻿using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long,Inventory>
    {
        EditInventory GetDetails(long id);
        Inventory GetProduct(long productId);
        List<InventoryViewModel> Search(InvnetorySearchModel searchModel);
    }
}
