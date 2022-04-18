using _MaralShopQuery.Contacts.ProductCategroy;
using _MaralShopQuery.Query;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategories = new MenuModel
            {
                ProductCategories = _productCategoryQuery.GetList()
            };
            return View("Default", productCategories);
        }
    }
}
