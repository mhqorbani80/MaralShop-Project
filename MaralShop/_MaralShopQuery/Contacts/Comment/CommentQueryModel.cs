namespace _MaralShopQuery.Contacts.Comment
{
    public class CommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Cancel { get; set; }
        public bool Confirm { get; set; }
    }
}
