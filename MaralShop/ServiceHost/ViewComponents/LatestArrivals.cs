using _MaralShopQuery.Contacts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivals : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestArrivals(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }
        public IViewComponentResult Invoke()
        {
            var products = _productQuery.GetLatestArrivals();
            return View("Defualt",products);
        }
    }
}
