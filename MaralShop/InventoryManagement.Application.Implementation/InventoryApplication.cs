using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application.Implementation
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation=new OperationResult();
            if (_inventoryRepository.Exists(i => i.ProductId == command.ProductId))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var inventory = new Inventory(command.ProductId,command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.Save();
            return operation.IsSuccess();
        }
        public OperationResult Edit(EditInventory command)
        {
            var operation=new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.Id);
            if (inventory == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if (_inventoryRepository.Exists(i => i.ProductId == command.ProductId &&
             i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.Save();
            return operation.IsSuccess();
        }
        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            const long OperatorId = 1;
            inventory.Increase(command.Count, OperatorId, command.Description);
            _inventoryRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Decrease(DecreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            const long OperatorId = 1;
            inventory.Decrease(command.Count, OperatorId, command.Description,0);
            _inventoryRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Decrease(List<DecreaseInventory> command)
        {
            var operation = new OperationResult();
            const long OperatorId = 1;
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Decrease(item.Count, OperatorId,item.Description, 0);
            }
            _inventoryRepository.Save();
            return operation.IsSuccess();
        }


        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }


        public List<InventoryViewModel> Search(InvnetorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);

        }
    }
}
