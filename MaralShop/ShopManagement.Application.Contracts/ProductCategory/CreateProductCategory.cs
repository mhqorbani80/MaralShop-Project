using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Name { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Description { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? MetaDescription { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Slug { get;  set; }
    }
}
