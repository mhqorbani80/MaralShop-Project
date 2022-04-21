using _0_Framework.Application;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext) :base(blogContext)
        {
            _blogContext = blogContext;
        }

        public EditArticle GetDetails(long id)
        {
            return _blogContext.Articles
                .Select(i=>new EditArticle
                { 
                 Id = i.Id,
                 Title=i.Title,
                 Description=i.Description,
                 ShortDescription=i.ShortDescription,
                 PictureAlt=i.PictureAlt,
                 PictureTitle=i.PictureTitle,
                 Keywords=i.Keywords,
                 MetaDescription=i.MetaDescription,
                 Slug=i.Slug,
                 CanonicalAddress=i.CanonicalAddress,
                 ArticleCategoryId=i.ArticleCategoryId,
                }).FirstOrDefault(i=>i.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var articles = _blogContext.Articles
                .Include(i=>i.ArticleCategory)
                .Select(i=>new ArticleViewModel
                {
                    Id=i.Id,
                    Title=i.Title,
                    Picture=i.Picture,
                    ShortDescription=i.ShortDescription.Substring(0, Math.Min(i.ShortDescription.Length, 50))+"...",
                    ArticleCategory=i.ArticleCategory.Name,
                    ArticleCategoryId=i.ArticleCategoryId,
                    CreationDate=i.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                articles=articles.Where(i=>i.Title.Contains(searchModel.Title));
            }
            if(searchModel.ArticleCategoryId != 0)
            {
                articles = articles.Where(i => i.ArticleCategoryId == searchModel.ArticleCategoryId);
            }
            return articles.OrderByDescending(i=>i.Id).ToList();
        }
    }
}
