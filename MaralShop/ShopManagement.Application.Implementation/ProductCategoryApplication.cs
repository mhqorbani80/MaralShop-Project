using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Implementation
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation=new OperationResult();
            if (_productCategoryRepository.Exists(i => i.Name == command.Name))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }

            var slug=command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name,command.Description,command.Picture,command.PictureAlt,
                command.PictureTitle,command.Keywords,command.MetaDescription,slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(command.Id);
            if(productCategory == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if(_productCategoryRepository.Exists(i=>i.Name == command.Name && i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,slug);

            _productCategoryRepository.Save();
            return operation.IsSuccess();
        }

        public EditProductCategory GetDetails(long id)
        {
           return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCateoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
