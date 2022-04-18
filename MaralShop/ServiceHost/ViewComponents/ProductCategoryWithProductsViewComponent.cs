using _MaralShopQuery.Contacts.ProductCategroy;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCtegoryWithProducts= _productCategoryQuery.GetProductCategoriesWithProducts();
            return View("Default",productCtegoryWithProducts);
        }
    }
}
