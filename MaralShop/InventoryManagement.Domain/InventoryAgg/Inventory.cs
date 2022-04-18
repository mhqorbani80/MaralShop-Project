using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public Inventory()
        {
            InventoryOperations = new List<InventoryOperation>();
        }
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }
        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(i => i.Operation).Sum(i => i.Count);
            var minus = InventoryOperations.Where(i => !i.Operation).Sum(i => i.Count);
            return plus - minus;
        }

        public void Increase(long count,long operatorId,string description)
        {
            var currentCount= CalculateCurrentCount() + count;
            var inventoryOperation = new InventoryOperation(true,count,operatorId,currentCount,description,0,Id);
            InventoryOperations.Add(inventoryOperation);
            InStock = currentCount > 0;
        }
        public void Decrease(long count, long operatorId, string description,long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var inventoryOperation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, Id);
            InventoryOperations.Add(inventoryOperation);
            InStock = currentCount > 0;
        }
    }

    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get;  set; } //ورودی خروجی
        public long Count { get;  set; }
        public long OperatorId { get;  set; }
        public long CurrentCount { get;  set; }
        public string Description { get;  set; }
        public long OrderId { get;  set; }
        public long InventoryId { get;  set; }
        public Inventory Inventory { get; set; }
        public DateTime OperationDate { get; set; }

        public InventoryOperation(bool operation, long count, long operatorId,
            long currentCount, string description, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }
}
