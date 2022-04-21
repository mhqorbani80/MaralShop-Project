using _MaralShopQuery.Contacts.ArticleCategory;
using _MaralShopQuery.Contacts.ProductCategroy;

namespace _MaralShopQuery.Query
{
    public class MenuModel
    {
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    }
}