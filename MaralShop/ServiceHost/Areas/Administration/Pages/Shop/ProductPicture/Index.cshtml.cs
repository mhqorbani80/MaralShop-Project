using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        public SelectList Products;
        public ProductPictureSearchModel SearchModel;
        public EditProductPicture EditProductPicture;
        public List<ProductPictureViewModel> ProductPictures;

        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictures = _productPictureApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var productPicture = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
        };
            return Partial("./Create", productPicture);
        }

        public JsonResult OnPostCreate(CreateProductPicture createProductPicture)
        {
            var result = _productPictureApplication.Create(createProductPicture);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditProductPicture= _productPictureApplication.GetDetails(id);
            EditProductPicture.Products = _productApplication.GetProducts();
            return Partial("./Edit", EditProductPicture);
        }

        public JsonResult OnPostEdit(EditProductPicture editProductPicture)
        {
            var result= _productPictureApplication.Edit(editProductPicture);
            return new JsonResult(result);
        }
    }
}
