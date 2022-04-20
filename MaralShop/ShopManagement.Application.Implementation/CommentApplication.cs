using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application.Implementation
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public OperationResult Create(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name,command.Email,command.Message,command.ProductId);
            _commentRepository.Create(comment);
            _commentRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.GetBy(id);
            if(comment == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            comment.Cancel();
            _commentRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.GetBy(id);
            if (comment == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            comment.Confirm();
            _commentRepository.Save();
            return operation.IsSuccess();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
