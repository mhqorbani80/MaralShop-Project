using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public EditProductCategory EditProductCategory;
        public List<ProductCateoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory createProductCategory)
        {
            var result = _productCategoryApplication.Create(createProductCategory);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditProductCategory=_productCategoryApplication.GetDetails(id);
            return Partial("./Edit", EditProductCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory editProductCategory)
        {
            var result=_productCategoryApplication.Edit(editProductCategory);
            return new JsonResult(result);
        }
    }
}
