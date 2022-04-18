using _MaralShopQuery.Contacts.Product;
using _MaralShopQuery.Contacts.ProductCategroy;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore;
using _0_Framework.Application;

namespace _MaralShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetList()
        {
            return _shopContext.ProductCategories
                .Select(i=> new ProductCategoryQueryModel
            {
                Name = i.Name,
                Picture=i.Picture,
                PictureAlt=i.PictureAlt,
                PictureTitle=i.PictureTitle,
                Slug=i.Slug,
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory=_inventoryContext.Inventories
                .Select(i=>new {i.Id,i.ProductId,i.UnitPrice}).ToList();

            var discounts=_discountContext.CustomerDiscounts
                .Where(i=>i.StartDate <= DateTime.Now && i.EndDate >= DateTime.Now)
                .Select(i=>new {i.ProductId,i.DiscountRate}).ToList();

            var productCategories= _shopContext.ProductCategories
                .Include(i=>i.Products)
                .ThenInclude(i=>i.ProductCategory)
                .Select(i=>new ProductCategoryQueryModel
            {
                Id=i.Id,
                Name=i.Name,
                Picture=i.Picture,
                PictureAlt=i.PictureAlt,
                PictureTitle=i.PictureTitle,
                Slug=i.Slug,
                Products=MapProducts(i.Products)
            }).ToList();

            foreach(var category in productCategories)
            {
                foreach(var product in category.Products)
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
        }
            return productCategories;
    }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products
                .Select(product => new ProductQueryModel()
            {
                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                slug=product.Slug,
                ProductCategory = product.ProductCategory.Name
            }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventories
                .Select(i => new { i.Id, i.ProductId, i.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(i => i.StartDate <= DateTime.Now && i.EndDate >= DateTime.Now)
                .Select(i => new { i.ProductId, i.DiscountRate,i.EndDate }).ToList();

            var productCategory = _shopContext.ProductCategories
                .Include(i => i.Products)
                .ThenInclude(i => i.ProductCategory)
                .Select(i => new ProductCategoryQueryModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description=i.Description,
                    Picture = i.Picture,
                    PictureAlt = i.PictureAlt,
                    PictureTitle = i.PictureTitle,
                    Keywords = i.Keywords,
                    MetaDescription=i.MetaDescription,
                    Slug = i.Slug,
                    Products = MapProducts(i.Products)
                }).FirstOrDefault(i=>i.Slug == slug);

                foreach (var product in productCategory.Products)
                {
                    var price = inventory.FirstOrDefault(i => i.ProductId == product.Id).UnitPrice;
                    product.Price = price.ToMoney();
                    var discount = discounts.FirstOrDefault(i => i.ProductId == product.Id);
                    if (discount == null) continue;
                    var discountRate = discount.DiscountRate;
                    product.DiscouuntRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToString();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    var resultPriceWithDiscount = discountAmount - price;
                    product.PriceWithDiscount = resultPriceWithDiscount.ToMoney();
                }
            return productCategory;
        }
    }
}
