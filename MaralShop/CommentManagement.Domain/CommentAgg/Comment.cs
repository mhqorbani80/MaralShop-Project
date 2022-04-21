using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirm { get; private set; }
        public bool IsCancel { get; private set; }
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }
        public List<Comment> Children { get; private set; }

        public Comment(string name, string email, string message,long ownerRecordId,int type,long parentId)
        {
            Name = name;
            Email = email;
            Message = message;
            IsConfirm = false;
            IsCancel = false;
            OwnerRecordId = ownerRecordId;
            Type = type;
            parentId = parentId;
        }

        public void Confirm()
        {
            IsConfirm = true;
            IsCancel = false;
        }
        public void Cancel()
        {
            IsConfirm = false;
            IsCancel = true;
        }
    }
}