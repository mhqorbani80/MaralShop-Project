using _0_Framework.Application;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryRepository(BlogContext blogContext) :base(blogContext)
        {
            _blogContext = blogContext;
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _blogContext.ArticleCategories.Select(i => new EditArticleCategory
            {
                Id =i.Id,
                Name=i.Name,
                Description=i.Description,
                PictureAlt=i.PictureAlt,
                PictureTitle=i.PictureTitle,
                ShowOrder=i.ShowOrder,
                Slug=i.Slug,
                Keywords=i.Keywords,
                MetaDescription=i.MetaDescription,
                CanonicalAddress=i.CanonicalAddress
            }).FirstOrDefault(i=>i.Id==id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var articleCategories = _blogContext.ArticleCategories
                .Select(i => new ArticleCategoryViewModel()
                {
                    Id = i.Id,
                    Description = i.Description,
                    Name = i.Name,
                    ShowOrder = i.ShowOrder,
                    Picture = i.Picture,
                    CreationDate = i.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                articleCategories = articleCategories.Where(i => i.Name.Contains(searchModel.Name));
            }


            return articleCategories.OrderByDescending(i => i.ShowOrder).ToList();
        }
    }
}