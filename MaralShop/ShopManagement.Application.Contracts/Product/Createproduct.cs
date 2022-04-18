using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        public string? Name { get;  set; }
        public string? Code { get;  set; }
        public string? ShortDescription { get;  set; }
        public string? Description { get;  set; }
        public IFormFile? Picture { get;  set; }
        public string? PictureAlt { get;  set; }
        public string? PictureTitle { get;  set; }
        public string? Keywords { get;  set; }
        public string? MetaDescription { get;  set; }
        public string? Slug { get;  set; }
        public long ProductCategoryId { get;  set; }
        public List<ProductCateoryViewModel> ProductCateories { get; set; }

    }
}
