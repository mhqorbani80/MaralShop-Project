using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _shopContext;

        public ProductRepository(ShopContext shopContext) :base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProduct GetDetails(long id)
        {
            return _shopContext.Products
                .Select(i => new EditProduct
            {
                    Id = i.Id,
                    Name = i.Name,
                    Code = i.Code,
                    UnitPrice=i.UnitPrice,
                    ShortDescription = i.ShortDescription,
                    Description = i.Description,
                    Picture=i.Picture,
                    PictureAlt=i.PictureAlt,
                    PictureTitle=i.PictureTitle,
                    Keywords=i.Keywords,
                    MetaDescription=i.MetaDescription,
                    ProductCategoryId=i.ProductCategoryId,
                    Slug=i.Slug
            }).FirstOrDefault(i => i.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _shopContext.Products
                .Include(i => i.ProductCategory)
                .Select(i => new ProductViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Code = i.Code,
                    UnitPrice=i.UnitPrice.ToString(),
                    ProductCategory = i.ProductCategory.Name,
                    PrductCategoryId = i.ProductCategoryId,
                    CreationDate=i.CreationDate.ToString()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(i => i.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(i => i.Code.Contains(searchModel.Code));
            }

            if (searchModel.ProductCategoryId != 0)
            {
                query = query.Where(i => i.PrductCategoryId == searchModel.ProductCategoryId);
            }

            return query.OrderByDescending(i => i.Id).ToList();

        }

         public List<ProductViewModel> GetAll()
        {
            return _shopContext.Products
                .Include(i => i.ProductCategory)
                .Select(i => new ProductViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Code = i.Code,
                    ProductCategory = i.ProductCategory.Name,
                    PrductCategoryId = i.ProductCategoryId
                }).ToList();
        }
    }
}
