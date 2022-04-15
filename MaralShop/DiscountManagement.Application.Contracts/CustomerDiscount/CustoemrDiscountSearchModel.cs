namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustoemrDiscountSearchModel
    {
        public long ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}