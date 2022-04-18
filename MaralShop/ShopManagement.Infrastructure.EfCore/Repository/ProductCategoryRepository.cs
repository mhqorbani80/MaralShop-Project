using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository(ShopContext shopContext) :base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProductCategory GetDetails(long id)
        {
            return _shopContext.ProductCategories.Select(i => new EditProductCategory
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Picture = i.Picture,
                PictureAlt = i.PictureAlt,
                PictureTitle = i.PictureTitle,
                Keywords = i.Keywords,
                MetaDescription = i.MetaDescription,
                Slug = i.Slug
            }).FirstOrDefault(i => i.Id == id);
        }
        public List<ProductCateoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _shopContext.ProductCategories.Select(i => new ProductCateoryViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Picture = i.Picture,
                ProductsCount= i.Products.Count().ToString(),
                CreationDate = i.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(i => i.Name.Contains(searchModel.Name));
            }
            return query.OrderByDescending(i=>i.Id).ToList();
        }

        List<ProductCateoryViewModel> IProductCategoryRepository.GetproductCategories()
        {
            return _shopContext.ProductCategories.Select(i => new ProductCateoryViewModel
            {
                Id = i.Id,
                Name = i.Name,
            }).ToList();
        }
    }
}
