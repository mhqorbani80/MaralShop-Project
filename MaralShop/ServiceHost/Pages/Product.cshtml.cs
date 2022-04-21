using _MaralShopQuery.Contacts.Product;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public IActionResult OnPost(AddComment command,string ProductSlug)
        {
            command.Type = CommentTypes.Product;
            _commentApplication.Create(command);
            return RedirectToPage("./Product", new {id= ProductSlug});
        }
    }
}