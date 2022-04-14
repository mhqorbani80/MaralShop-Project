using _MaralShopQuery.Contacts.ProductCategroy;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MaralShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
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
    }
}
