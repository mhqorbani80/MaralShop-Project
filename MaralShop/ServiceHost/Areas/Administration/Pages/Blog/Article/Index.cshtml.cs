using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {

        public SelectList ArticleCategories;
        public ArticleSearchModel SearchModel;
        public EditArticle EditArticle;
        public List<ArticleViewModel> Articles;
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.Search(searchModel);
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var article = new CreateArticle()
            {
                ArticleCategories = _articleCategoryApplication.GetArticleCategories()
        };
            return Partial("./Create",article);
        }

        public JsonResult OnPostCreate(CreateArticle createArticle)
        {
            var result = _articleApplication.Create(createArticle);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var EditArticle = _articleApplication.GetDetails(id);
            return Partial("./Edit", EditArticle);
        }

        public JsonResult OnPostEdit(EditArticle editArtcle)
        {
            var result = _articleApplication.Edit(editArtcle);
            return new JsonResult(result);
        }
    }
}
