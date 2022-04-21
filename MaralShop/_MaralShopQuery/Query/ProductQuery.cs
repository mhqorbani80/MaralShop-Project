using _0_Framework.Application;
using _MaralShopQuery.Contacts.Comment;
using _MaralShopQuery.Contacts.Product;
using _MaralShopQuery.Contacts.ProductPicture;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _MaralShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
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
                     ProductCategory = product.ProductCategory.Name,
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

        public ProductQueryModel GetProduct(string slug)
        {
            var inventory = _inventoryContext.Inventories
                .Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductCategory = x.ProductCategory.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    slug = x.Slug,
                    ProductCategorySlug = x.ProductCategory.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    ProductPictures = MapProductPicture(x.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                //product. = price;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscouuntRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            product.Comments = _commentContext.Comments
                .Where(x => !x.IsCancel)
                .Where(x => x.IsConfirm)
                .Where(x => x.Type == CommentTypes.Product)
                .Where(x => x.OwnerRecordId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        //private static List<CommentQueryModel> MapComments(List<Comment> comments)
        //{
        //    return comments
        //        .Where(i=> !i.IsCancel)
        //        .Where(i=>i.IsConfirm)
        //       .Select(i => new CommentQueryModel
        //       {
        //           Name= i.Name,
        //           Email= i.Email,
        //           Message=i.Message,
        //           Cancel=i.IsCancel,
        //           Confirm=i.IsConfirm
        //       }).ToList();
        //}

        private static List<ProductPictureQueryModel> MapProductPicture(List<ProductPicture> productPictures)
        {
            return productPictures
                .Select(i=>new ProductPictureQueryModel
                {
                    Picture=i.Picture,
                    IsRemove=i.IsRemove,
                    PictureAlt=i.PictureAlt,
                    PictureTitle=i.PictureTitle
                }).ToList();
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