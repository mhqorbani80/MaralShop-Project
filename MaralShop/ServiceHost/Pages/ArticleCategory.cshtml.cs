using _MaralShopQuery.Contacts.Article;
using _MaralShopQuery.Contacts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public List<ArticleQueryModel> Articles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public ArticleCategoryQueryModel ArticleCategory;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            Articles = _articleQuery.GetLatestArticles();
            ArticleCategory = _articleCategoryQuery.GetDetails(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }
    }
}