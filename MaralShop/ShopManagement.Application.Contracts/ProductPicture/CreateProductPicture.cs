using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get;  set; }

        [MaxFileSize(3 * 1024 * 1024,ErrorMessage =ValidationMessage.MaxFileSizeError)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}