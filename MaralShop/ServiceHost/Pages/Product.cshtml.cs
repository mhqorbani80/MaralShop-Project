using _MaralShopQuery.Contacts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;

        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProduct(id);
        }
        public IActionResult OnPostAdd(AddComment command,string ProductSlug)
        {
           var comment= _commentApplication.Create(command);
            return RedirectToPage("./Product", new {id= ProductSlug});
        }
    }
}