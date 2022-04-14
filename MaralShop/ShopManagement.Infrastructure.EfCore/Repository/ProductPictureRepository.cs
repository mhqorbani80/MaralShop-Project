using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using Microsoft.EntityFrameworkCore;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _shopContext;

        public ProductPictureRepository(ShopContext shopContext) :base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _shopContext.ProductPictures.Select(i=>new EditProductPicture
            {
                Id = i.Id,
                Picture = i.Picture,
                PictureAlt = i.PictureAlt,
                PictureTitle = i.PictureTitle,
                ProductId=i.ProductId
            }).FirstOrDefault(i=>i.Id==id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _shopContext.ProductPictures
                .Include(i=>i.Product)
                .Select(i => new ProductPictureViewModel
            {
                Id=i.Id,
                Picture=i.Picture,
                Product=i.Product.Name,
                ProductId=i.ProductId,
                CreationDate=i.CreationDate.ToString()
            });
            if(searchModel.ProductId != 0)
            {
                query = query.Where(i => i.ProductId == searchModel.ProductId);
            }
            return query.OrderByDescending(i => i.Id).ToList();
        }
    }
}