using _0_Framework.Application;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long,Comment> , ICommentRepository
    {
        private readonly ShopContext _shopContext;

        public CommentRepository(ShopContext shopContext) :base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var comments= _shopContext.Comments
                .Include(i=>i.Product)
                .Select(i=>new CommentViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Email = i.Email,
                    Message = i.Message,
                    Cancel = i.IsCancel,
                    Confirm = i.IsConfirm,
                    Product=i.Product.Name,
                    ProductId=i.ProductId,
                    CreationDate=i.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                comments=comments.Where(i=>i.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                comments = comments.Where(i => i.Email.Contains(searchModel.Email));
            }
            return comments.OrderByDescending(i=>i.Id).ToList();
        }
    }
}