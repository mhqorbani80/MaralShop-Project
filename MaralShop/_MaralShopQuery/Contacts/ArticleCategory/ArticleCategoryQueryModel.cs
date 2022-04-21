using _MaralShopQuery.Contacts.Article;

namespace _MaralShopQuery.Contacts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public List<String> KeywordsList { get; set; }
        public long ArticlesCount { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }

    }
}