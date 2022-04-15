using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application.Implementation
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Create(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if(_customerDiscountRepository.Exists(i=>i.ProductId == command.ProductId && i.DiscountRate == command.DiscountRate))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate,
                startDate, endDate, command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetBy(command.Id);
            if(customerDiscount == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if (_customerDiscountRepository.Exists(i => i.ProductId == command.ProductId && 
            i.DiscountRate == command.DiscountRate && i.Id !=command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId, command.DiscountRate,
                startDate, endDate, command.Reason);
            _customerDiscountRepository.Save();
            return operation.IsSuccess();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustoemrDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}