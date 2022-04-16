namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public string DiscountRate { get; set; }
        public string StartDate { get; set; }
        //Georgian
        public DateTime StartDateGr { get; set; }
        public string EndDate { get; set; }
        public DateTime EndDateGr { get; set; }
        public string Reason { get; set; }
        public string CreationDate { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; } 
    }
}