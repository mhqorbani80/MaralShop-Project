namespace _MaralShopQuery.Contacts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetDetails(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
