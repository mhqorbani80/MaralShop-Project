using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.Implementation
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUpload _fileUpload;

        public ProductApplication(IProductRepository productRepository, IFileUpload fileUpload)
        {
            _productRepository = productRepository;
            _fileUpload = fileUpload;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(i => i.Name == command.Name))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var pathPicture= $"{command.Slug}";
            var path = _fileUpload.Upload(command.Picture, pathPicture);
            var slug = command.Slug.Slugify();
            var product = new Product(command.Name,command.Code,command.ShortDescription,command.Description,
                path, command.PictureAlt,command.PictureTitle,
                command.Keywords,command.MetaDescription,slug,command.ProductCategoryId);

            _productRepository.Create(product);
            _productRepository.Save();

            return operation.IsSuccess();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetBy(command.Id);
            if(product == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if(_productRepository.Exists(i=>i.Name == command.Name && i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var pathPicture = $"{command.Slug}";
            var path = _fileUpload.Upload(command.Picture, pathPicture);
            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.Code, command.ShortDescription, command.Description,
                path, command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug, command.ProductCategoryId);

            _productRepository.Save();

            return operation.IsSuccess();
        }


        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }
        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
