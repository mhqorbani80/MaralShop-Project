using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Implementation
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation=new OperationResult();
            if(_productPictureRepository.Exists(i=>i.Picture == command.Picture && i.ProductId == command.ProductId))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var productPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetBy(command.Id);
            if(productPicture == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if(_productPictureRepository.Exists(i => i.Picture == command.Picture && i.ProductId == command.ProductId && i.Id == command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Save();
            return operation.IsSuccess();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            productPicture.Remove();
            _productPictureRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            productPicture.Restore();
            _productPictureRepository.Save();
            return operation.IsSuccess();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
