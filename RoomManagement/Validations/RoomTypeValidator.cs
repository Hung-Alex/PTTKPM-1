using FluentValidation;
using RoomManagement.Areas.Admin.Models.RoomModel;
using RoomManagement.Areas.Admin.Models.RoomTypeModel;
using RoomManagement.Services.RoomMangementService.RoomTypeSerivce;

namespace RoomManagement.Validations
{
    public class RoomTypeValidator : AbstractValidator<RoomTypeEditModel>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomTypeValidator(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;

            RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Tên loại phòng  không được để trống")
            .MaximumLength(500)
            .WithMessage("Tên loại phòng  dài tối đa '{MaxLength}'");

            RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Mô tả về Loại phòng  không được để trống");

            RuleFor(p => p.UrlSlug)
            .NotEmpty()
            .WithMessage("Slug của Loại phòng không được để trống")
            .MaximumLength(1000)
            .WithMessage("Slug dài tối đa '{MaxLength}'");

            RuleFor(p => p.UrlSlug)
            .MustAsync(async (roomTypeModel,slug, cancellationToken) => !await _roomTypeRepository.IsRoomTypeSlugExistedAsync(roomTypeModel.Id, slug, cancellationToken))
            .WithMessage("Slug '{PropertyValue}' đã được sử dụng");
        }
    }
}
