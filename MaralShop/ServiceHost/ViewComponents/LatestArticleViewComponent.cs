using _MaralShopQuery.Contacts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticleViewComponent : ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public LatestArticleViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.GetArticles();
            return View("./Default",articles);
        }
    }
}