using FluentValidation;
using RoomManagement.Areas.Admin.Models.VoucherModel;
using RoomManagement.Services.RoomMangementService.VoucherSerivce;

namespace RoomManagement.Validations
{
    public class VoucherValidator:AbstractValidator<VoucherEditModel>
    {
        private readonly IVoucherRepository _voucherRepository;
        public VoucherValidator(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;

            RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Tên voucher  không được để trống")
            .MaximumLength(500)
            .WithMessage("Tên voucher  dài tối đa '{MaxLength}'");

            RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("voucher phòng  không được để trống");

            RuleFor(p => p.UrlSlug)
            .NotEmpty()
            .WithMessage("Slug của voucher không được để trống")
            .MaximumLength(1000)
            .WithMessage("Slug dài tối đa '{MaxLength}'");
            RuleFor(p => p.Discount)
                .LessThanOrEqualTo(100)
                .WithMessage("Phần trăm giảm giá phải nhỏ hơn hoặc bằng  100")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Phần trăm giảm giá phải lớn hơn hoặc bằng 0");

            RuleFor(p => p.UrlSlug)
            .MustAsync(async (voucherModel, slug, cancellationToken) => !await _voucherRepository.IsVoucherSlugExistedAsync(voucherModel.Id, slug, cancellationToken))
            .WithMessage("Slug '{PropertyValue}' đã được sử dụng");
        }
    }
}
