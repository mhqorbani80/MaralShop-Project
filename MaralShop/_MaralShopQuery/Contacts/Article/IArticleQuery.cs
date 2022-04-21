namespace _MaralShopQuery.Contacts.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetDeatils(string slug);
        List<ArticleQueryModel> GetArticles();
        List<ArticleQueryModel> GetLatestArticles();
    }
}
