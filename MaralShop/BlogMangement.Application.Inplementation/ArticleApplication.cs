using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;

namespace BlogMangement.Application.Inplementation
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUpload _fileUpload;

        public ArticleApplication(IArticleRepository articleRepository, IFileUpload fileUpload)
        {
            _articleRepository = articleRepository;
            _fileUpload = fileUpload;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exists(i => i.Title == command.Title))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var pathPicture = $"{command.Slug}";
            var path = _fileUpload.Upload(command.Picture, pathPicture);
            var slug = command.Slug.Slugify();
            var article = new Article(command.Title,command.ShortDescription,command.Description, path, command.PictureAlt,
                command.PictureTitle,command.Slug,command.Keywords,command.MetaDescription,command.CanonicalAddress,command.ArticleCategoryId);

            _articleRepository.Create(article);
            _articleRepository.Save();

            return operation.IsSuccess();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var product = _articleRepository.GetBy(command.Id);
            if (product == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if (_articleRepository.Exists(i => i.Title == command.Title && i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var pathPicture = $"{command.Slug}";
            var path = _fileUpload.Upload(command.Picture, pathPicture);
            var slug = command.Slug.Slugify();
            product.Edit(command.Title, command.ShortDescription, command.Description, path, command.PictureAlt,
                command.PictureTitle, command.Slug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress, command.ArticleCategoryId);

            _articleRepository.Save();

            return operation.IsSuccess();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
