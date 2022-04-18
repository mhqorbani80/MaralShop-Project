using _0_Framework.Application;
using _MaralShopQuery.Contacts.Product;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace _MaralShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventories
               .Select(i => new { i.Id, i.ProductId, i.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(i => i.StartDate <= DateTime.Now && i.EndDate >= DateTime.Now)
                .Select(i => new { i.ProductId, i.DiscountRate }).ToList();

            var products=_shopContext.Products
                .Select(product => new ProductQueryModel()
                 {
                     Id = product.Id,
                     Name = product.Name,
                     Picture = product.Picture,
                     PictureAlt = product.PictureAlt,
                     PictureTitle = product.PictureTitle,
                     slug = product.Slug,
                     ProductCategory = product.ProductCategory.Name
                 }).OrderByDescending(i=>i.Id).Take(6).ToList();

            foreach (var product in products)
            {
                var price = inventory.FirstOrDefault(i => i.ProductId == product.Id).UnitPrice;
                product.Price = price.ToMoney();
                var discount = discounts.FirstOrDefault(i => i.ProductId == product.Id);
                if (discount == null) continue;
                var discountRate = discount.DiscountRate;
                product.DiscouuntRate = discountRate;
                product.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round((price * discountRate) / 100);
                var resultPriceWithDiscount = discountAmount - price;
                product.PriceWithDiscount = resultPriceWithDiscount.ToMoney();
            }
            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventories
              .Select(i => new { i.Id, i.ProductId, i.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(i => i.StartDate <= DateTime.Now && i.EndDate >= DateTime.Now)
                .Select(i => new { i.ProductId, i.DiscountRate }).ToList();

            var query = _shopContext.Products
                .Include(i=>i.ProductCategory)
                .Select(product => new ProductQueryModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    slug = product.Slug,
                    ProductCategory = product.ProductCategory.Name,
                    ProductCategorySlug = product.ProductCategory.Slug
                });

            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query.Where(i => i.Name.Contains(value));
            }
            var products = query.OrderByDescending(i=>i.Id)
                .ToList();

            foreach (var product in products)
            {
                var price = inventory.FirstOrDefault(i => i.ProductId == product.Id).UnitPrice;
                product.Price = price.ToMoney();
                var discount = discounts.FirstOrDefault(i => i.ProductId == product.Id);
                if (discount == null) continue;
                var discountRate = discount.DiscountRate;
                product.DiscouuntRate = discountRate;
                product.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round((price * discountRate) / 100);
                var resultPriceWithDiscount = discountAmount - price;
                product.PriceWithDiscount = resultPriceWithDiscount.ToMoney();
            }
            return products;
        }
    }
}