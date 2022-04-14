namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get;  set; }
        public string Picture { get;  set; }
        public long PictureAlt { get;  set; }
        public long PictureTitle { get;  set; }
    }
}