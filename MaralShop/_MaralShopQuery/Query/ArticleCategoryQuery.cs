using _0_Framework.Application;
using _MaralShopQuery.Contacts.Article;
using _MaralShopQuery.Contacts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _MaralShopQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext.ArticleCategories
                .Include(i=>i.Articles)
                .Select(i=>new ArticleCategoryQueryModel
            {
                Id = i.Id,
                Name=i.Name,
                Slug=i.Slug,
                ArticlesCount =i.Articles.Count()
            }).ToList();
        }

        public ArticleCategoryQueryModel GetDetails(string slug)
        {

            var articleCategory = _blogContext.ArticleCategories
                .Select(i => new ArticleCategoryQueryModel
                {
                    Id=i.Id,
                    Name =i.Name,
                    Slug =i.Slug,
                    Keywords=i.Keywords,
                    Articles=MapArticles(i.Articles)
                }).FirstOrDefault(i => i.Slug == slug);


            articleCategory.KeywordsList = articleCategory.Keywords.Split(",").ToList();

            return articleCategory;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles
                .Select(i=>new ArticleQueryModel
            {
                Id=i.Id,
                Title=i.Title,
                Picture=i.Picture,
                PictureAlt=i.PictureAlt,
                PictureTitle=i.PictureTitle,
                Slug=i.Slug,
                CanonicalAddress=i.CanonicalAddress,
                ShortDescription=i.ShortDescription,
                CreationDate=i.CreationDate.ToFarsi(),
            }).OrderByDescending(i=>i.Id).ToList();

        }
    }
}
