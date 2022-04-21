using _0_Framework.Application;
using _0_Framework.Domain;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext) : base(commentContext)
        {
            _commentContext = commentContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var comments = _commentContext.Comments
                .Select(i => new CommentViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Email = i.Email,
                    Message = i.Message,
                    Cancel = i.IsCancel,
                    Confirm = i.IsConfirm,
                    OwnerRecordId = i.OwnerRecordId,
                    Type=i.Type,
                    CreationDate = i.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                comments = comments.Where(i => i.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                comments = comments.Where(i => i.Email.Contains(searchModel.Email));
            }
            return comments.OrderByDescending(i => i.Id).ToList();
        }
    }
}