using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _shopContext;

        public SlideRepository(ShopContext shopContext) :base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditSlide GetDetails(long id)
        {
            return _shopContext.Slides.Select(i => new EditSlide
            {
                Id = i.Id,
                Picture = i.Picture,
                PictureAlt = i.PictureAlt,
                PictureTitle = i.PictureTitle,
                Heading = i.Heading,
                Text = i.Text,
                Title = i.Title,
                Description = i.Description,
                BtnText = i.BtnText,
                Link=i.Link
            }).FirstOrDefault(i => i.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _shopContext.Slides.Select(i => new SlideViewModel
            {
                Id=i.Id,
                Heading=i.Heading,
                Picture=i.Picture,
                IsRemove=i.IsRemove
            }).OrderByDescending(i=>i.Id).ToList();
        }
    }
}
