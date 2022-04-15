namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public bool IsRemove { get; set; }
        public string DiscountRate { get; set; }
        public string CreationDate { get; set; }
    }
}