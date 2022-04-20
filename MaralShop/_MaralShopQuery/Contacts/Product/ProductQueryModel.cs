using _MaralShopQuery.Contacts.Comment;
using _MaralShopQuery.Contacts.ProductPicture;

namespace _MaralShopQuery.Contacts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Price { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscouuntRate { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategorySlug { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string slug { get; set; }
        public bool HasDiscount { get; set; }
        public string DiscountExpireDate { get; set; }
        public bool IsInStock { get; set; }
        public long CommentsCount { get; set; }
        public List<ProductPictureQueryModel> ProductPictures { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
    }
}
