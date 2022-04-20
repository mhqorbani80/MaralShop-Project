namespace ShopManagement.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public bool Confirm { get; set; }
        public bool Cancel { get; set; }
        public string CreationDate { get; set; }
    }
}
