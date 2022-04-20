using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogMangement.Application.Inplementation
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUpload _fileUpload;

        public ArticleCategoryApplication(IFileUpload fileUpload, IArticleCategoryRepository articleCategoryRepository)
        {
            _fileUpload = fileUpload;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(i => i.Name == command.Name))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var path = $"{command.Slug}";
            var picturePath = _fileUpload.Upload(command.Picture, path);
            var slug = command.Slug.Slugify();
            var productCategory = new ArticleCategory(command.Name, picturePath, command.PictureAlt,command.PictureTitle,
                command.Description,command.ShowOrder,command.Keywords,command.MetaDescription,
                command.Slug,command.CanonicalAddress);

            _articleCategoryRepository.Create(productCategory);
            _articleCategoryRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.GetBy(command.Id);
            if (articleCategory == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if (_articleCategoryRepository.Exists(i => i.Name == command.Name && i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var path = $"{command.Slug}";
            var picturePath = _fileUpload.Upload(command.Picture, path);
            var slug = command.Slug.Slugify();
            articleCategory.Edit(command.Name, picturePath, command.PictureAlt, command.PictureTitle,
                command.Description, command.ShowOrder, command.Keywords, command.MetaDescription,
                command.Slug, command.CanonicalAddress);

            _articleCategoryRepository.Save();
            return operation.IsSuccess();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}