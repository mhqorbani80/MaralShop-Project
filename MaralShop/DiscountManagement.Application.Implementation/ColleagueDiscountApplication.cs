using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application.Implementation
{
    public class ColleagueDiscountApplication : IColleagueDisconutApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Create(DefineColleagueDiscount command)
        {
            var operation= new OperationResult();
            if(_colleagueDiscountRepository.Exists(i=>i.ProductId==command.ProductId && i.DiscountRate == command.DiscountRate))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var colleagueDiscount=new ColleagueDiscount(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation= new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(command.Id);
            if (colleagueDiscount == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if (_colleagueDiscountRepository.Exists(i => i.ProductId == command.ProductId &&
            i.DiscountRate == command.DiscountRate && i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Save();
            return operation.IsSuccess();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
           return _colleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);
            if (colleagueDiscount == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            colleagueDiscount.Remove();
            _colleagueDiscountRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);
            if (colleagueDiscount == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            colleagueDiscount.Restore();
            _colleagueDiscountRepository.Save();
            return operation.IsSuccess(); 
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
