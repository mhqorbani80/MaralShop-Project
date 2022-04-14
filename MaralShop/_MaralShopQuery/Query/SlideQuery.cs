using _MaralShopQuery.Contacts.Slide;
using ShopManagement.Infrastructure.EfCore;

namespace _MaralShopQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetList()
        {
            return _shopContext.Slides
                .Where(i=>i.IsRemove==false)
                .Select(i=>new SlideQueryModel {
                BtnText = i.BtnText,
                Description = i.Description,
                Heading=i.Heading,
                Link=i.Link,
                Picture=i.Picture,
                PictureAlt=i.PictureAlt,
                PictureTitle=i.PictureTitle,
                Text=i.Text,
                Title=i.Title,
            }).ToList();
        }
    }
}
