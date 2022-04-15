using _0_Framework.Application;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts
                .Select(i => new EditCustomerDiscount
                {
                    Id = id,
                    DiscountRate = i.DiscountRate,
                    StartDate = i.StartDate.ToFarsi(),
                    EndDate = i.EndDate.ToFarsi(),
                    ProductId = i.ProductId,
                    Reason = i.Reason,
                }).FirstOrDefault(i => i.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustoemrDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(i => new { i.Id, i.Name }).ToList();
            var query = _discountContext.CustomerDiscounts.Select(i => new CustomerDiscountViewModel
            {
                Id=i.Id,
                StartDate=i.StartDate.ToFarsi(),
                StartDateGr=i.StartDate,
                EndDate=i.EndDate.ToFarsi(),
                EndDateGr=i.EndDate,
                ProductId=i.ProductId,
                Reason=i.Reason,
                DiscountRate=i.DiscountRate.ToString(),
                CreationDate=i.CreationDate.ToFarsi()
            });

            if(searchModel.ProductId > 0)
            {
                query=query.Where(i=>i.ProductId==searchModel.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.StartDate)) // 1401/1/27 start => 27 searchmodel
            {
                query = query.Where(i => i.StartDateGr >= searchModel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate)) 
            {
                query = query.Where(i => i.EndDateGr >= searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(i=>i.Id).ToList();
            discounts.ForEach(
                discount => discount.Product = products
                .FirstOrDefault(product => product.Id == discount.ProductId)?.Name);

            return discounts;
        
        }
    }
}