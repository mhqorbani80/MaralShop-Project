using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        public CustoemrDiscountSearchModel SearchModel;
        public SelectList Products;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustoemrDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var customerDiscount = new DefineCustomerDiscount
            {
                Products = new SelectList(_productApplication.GetProducts(), "Id", "Name")
            };
            return Partial("./Create", customerDiscount);
        }
        public JsonResult OnPostCreate(DefineCustomerDiscount customerDiscount)
        {
            var result = _customerDiscountApplication.Create(customerDiscount);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return Partial("./Edit",customerDiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount customerDiscount)
        {
            var result = _customerDiscountApplication.Edit(customerDiscount);
            return new JsonResult(result);
        }
    }
}
