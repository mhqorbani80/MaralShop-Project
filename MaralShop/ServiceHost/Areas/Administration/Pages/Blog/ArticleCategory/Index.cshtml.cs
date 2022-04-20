using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel;
        public EditArticleCategory EditArticleCategory;
        public List<ArticleCategoryViewModel> ArticleCategories;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory createArticleCategory)
        {
            var result = _articleCategoryApplication.Create(createArticleCategory);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           var EditArticleCategory= _articleCategoryApplication.GetDetails(id);
            return Partial("./Edit", EditArticleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory editArtcleCategory)
        {
            var result= _articleCategoryApplication.Edit(editArtcleCategory);
            return new JsonResult(result);
        }
    }
}
