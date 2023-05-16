using FluentValidation;
using RoomManagement.Areas.Admin.Models.PriceManagementModel;
using RoomManagement.Services.RoomMangementService.PriceManagementSerivce;

namespace RoomManagement.Validations
{
    public class PriceManagementValidator:AbstractValidator<PriceManagementEditModel>
    {
        private readonly IPriceManagementRepository _priceManagementRepository;
        public PriceManagementValidator(IPriceManagementRepository priceManagementRepository)
        {
            _priceManagementRepository = priceManagementRepository;
            RuleFor(p => p.Name)
          .NotEmpty()
          .WithMessage("Tên Giá phòng  không được để trống")
          .MaximumLength(500)
          .WithMessage("Tên Giá phòng  dài tối đa '{MaxLength}'");

            RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Giá phòng  không được để trống");

            RuleFor(p => p.UrlSlug)
            .NotEmpty()
            .WithMessage("Slug của Giá phòng  không được để trống")
            .MaximumLength(1000)
            .WithMessage("Slug dài tối đa '{MaxLength}'");

            RuleFor(p => p.UrlSlug)
            .MustAsync(async (priceManagementModel, slug, cancellationToken) => !await _priceManagementRepository.IsPriceManagementSlugExistedAsync(priceManagementModel.Id, slug, cancellationToken))
            .WithMessage("Slug '{PropertyValue}' đã được sử dụng");
        }
    }
}
