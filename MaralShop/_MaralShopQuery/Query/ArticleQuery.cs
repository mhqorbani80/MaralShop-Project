using _0_Framework.Application;
using _MaralShopQuery.Contacts.Article;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _MaralShopQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        public readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleQueryModel> GetArticles()
        {
            return _blogContext.Articles
                .Include(i=>i.ArticleCategory)
                .Select(i=>new ArticleQueryModel
                {
                    Id = i.Id,
                    Title = i.Title,
                    ArticleCategory = i.ArticleCategory.Name,
                    ArticleCategoryId = i.ArticleCategoryId,
                    ArticleCategorySlug=i.ArticleCategory.Slug,
                    Slug=i.Slug,
                    CanonicalAddress=i.CanonicalAddress,
                    Description=i.Description,
                    Keywords=i.Keywords,
                    MetaDescription=i.MetaDescription,
                    Picture=i.Picture,
                    PictureAlt=i.PictureAlt,
                    PictureTitle=i.PictureTitle,
                    ShortDescription=i.ShortDescription,
                    CreationDate=i.CreationDate.ToFarsi()
                }).OrderByDescending(i=>i.Id).ToList();
        }

        public ArticleQueryModel GetDeatils(string slug)
        {
            var article =  _blogContext.Articles
                .Include(i => i.ArticleCategory)
                .Select(i => new ArticleQueryModel
                {
                    Id = i.Id,
                    Title = i.Title,
                    ArticleCategory = i.ArticleCategory.Name,
                    ArticleCategoryId = i.ArticleCategoryId,
                    ArticleCategorySlug = i.ArticleCategory.Slug,
                    Slug = i.Slug,
                    CanonicalAddress = i.CanonicalAddress,
                    Description = i.Description,
                    Keywords = i.Keywords,
                    MetaDescription = i.MetaDescription,
                    Picture = i.Picture,
                    PictureAlt = i.PictureAlt,
                    PictureTitle = i.PictureTitle,
                    ShortDescription = i.ShortDescription,
                    CreationDate = i.CreationDate.ToFarsi()
                }).FirstOrDefault(i=>i.Slug==slug);

            article.KeywordsList=article.Keywords.Split(",").ToList();

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
               .Include(i => i.ArticleCategory)
               .Select(i => new ArticleQueryModel
               {
                   Id = i.Id,
                   Title = i.Title,
                   ArticleCategory = i.ArticleCategory.Name,
                   ArticleCategoryId = i.ArticleCategoryId,
                   ArticleCategorySlug = i.ArticleCategory.Slug,
                   Slug = i.Slug,
                   CanonicalAddress = i.CanonicalAddress,
                   Description = i.Description,
                   Keywords = i.Keywords,
                   MetaDescription = i.MetaDescription,
                   Picture = i.Picture,
                   PictureAlt = i.PictureAlt,
                   PictureTitle = i.PictureTitle,
                   ShortDescription = i.ShortDescription,
                   CreationDate = i.CreationDate.ToFarsi()
               }).OrderByDescending(i => i.Id).Take(5).ToList();
        }
    }
}