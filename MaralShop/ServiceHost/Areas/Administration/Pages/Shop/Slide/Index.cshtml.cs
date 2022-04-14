using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        public List<SlideViewModel> Slides;
        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }
        public JsonResult OnPostCreate(CreateSlide createSlide)
        {
            var result=_slideApplication.Create(createSlide);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var slide=_slideApplication.GetDetails(id);
            return Partial("./Edit",slide);
        }
        public JsonResult OnPostEdit(EditSlide editSlide)
        {
            var result=_slideApplication.Edit(editSlide);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            _slideApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetReStore(long id)
        {
            _slideApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
