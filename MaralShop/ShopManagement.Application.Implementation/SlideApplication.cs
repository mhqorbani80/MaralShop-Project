using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Implementation
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            if(_slideRepository.Exists(i=>i.Picture == command.Picture))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            var slide = new Slide(command.Picture,command.PictureAlt,command.PictureTitle,command.Heading,
                command.Title,command.Text,command.Description,command.BtnText,command.Link);
            _slideRepository.Create(slide);
            _slideRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetBy(command.Id);
            if(slide == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            if(_slideRepository.Exists(i => i.Picture == command.Picture && i.Id != command.Id))
            {
                return operation.IsFaild(ApplicationMessage.DuplicatedData);
            }
            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.Description, command.BtnText, command.Link);
            _slideRepository.Save();
            return operation.IsSuccess();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetBy(id);
            if (slide == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            slide.Remove();
            _slideRepository.Save();
            return operation.IsSuccess();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetBy(id);
            if (slide == null)
            {
                return operation.IsFaild(ApplicationMessage.NotFountData);
            }
            slide.Restore();
            _slideRepository.Save();
            return operation.IsSuccess();
        }
    }
}