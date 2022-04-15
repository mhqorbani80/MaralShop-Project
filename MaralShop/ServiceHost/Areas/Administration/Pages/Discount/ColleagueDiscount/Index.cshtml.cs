using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        public SelectList Products;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public ColleagueDiscountSearchModel SearchModel;
        private readonly IColleagueDisconutApplication _colleagueDisconutApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IColleagueDisconutApplication colleagueDisconutApplication, IProductApplication productApplication)
        {
            _colleagueDisconutApplication = colleagueDisconutApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            ColleagueDiscounts = _colleagueDisconutApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
        public IActionResult OnGetCreate()
        {
            var customerDiscount = new DefineColleagueDiscount
            {
                Products = new SelectList(_productApplication.GetProducts(), "Id", "Name")
            };
            return Partial("./Create", customerDiscount);
        }
        public JsonResult OnPostCreate(DefineColleagueDiscount customerDiscount)
        {
            var result = _colleagueDisconutApplication.Create(customerDiscount);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _colleagueDisconutApplication.GetDetails(id);
            customerDiscount.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return Partial("./Edit", customerDiscount);
        }
        public JsonResult OnPostEdit(EditColleagueDiscount customerDiscount)
        {
            var result = _colleagueDisconutApplication.Edit(customerDiscount);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            _colleagueDisconutApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            _colleagueDisconutApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
