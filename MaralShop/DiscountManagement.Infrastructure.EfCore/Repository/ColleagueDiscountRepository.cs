using _0_Framework.Application;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;
        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(i=>new EditColleagueDiscount
            {
                Id = i.Id,
                DiscountRate = i.DiscountRate,
                ProductId = i.ProductId
            }).FirstOrDefault(i=>i.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(i => new { i.Id, i.Name }).ToList();
            var query = _discountContext.ColleagueDiscounts.Select(i => new ColleagueDiscountViewModel
            {
                Id = i.Id,
                ProductId = i.ProductId,
                IsRemove = i.IsRemove,
                CreationDate = i.CreationDate.ToFarsi(),
                DiscountRate = i.DiscountRate.ToString()
            });

            if (searchModel.ProductId > 0)
            {
                query = query.Where(i => i.ProductId == searchModel.ProductId);
            }
            var discounts = query.OrderByDescending(i => i.Id).ToList();
            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(product => product.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}
