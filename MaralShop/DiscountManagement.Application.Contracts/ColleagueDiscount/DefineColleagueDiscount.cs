using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get;  set; }
        public int DiscountRate { get;  set; }
        public SelectList Products { get; set; }
    }
}