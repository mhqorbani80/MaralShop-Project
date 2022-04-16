using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? UnitPrice { get; set; }
        public string? CreationDate { get; set; }
        public long PrductCategoryId { get; set; }
        public string? ProductCategory { get; set; }
    }
}
