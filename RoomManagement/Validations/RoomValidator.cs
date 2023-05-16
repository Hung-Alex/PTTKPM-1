using FluentValidation;
using RoomManagement.Areas.Admin.Models.RoomModel;
using RoomManagement.Services.RoomMangementService.RoomSerivce;

namespace RoomManagement.Validations
{
    public class RoomValidator : AbstractValidator<RoomEditModel>
    {
        private readonly IRoomRepository _roomRepository;
        public RoomValidator(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;

            RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Tên  của phòng  không được để trống")
            .MaximumLength(500)
            .WithMessage("Tiêu đề dài tối đa '{MaxLength}'");



            RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Mô tả về phòng không được để trống");


            RuleFor(p => p.UrlSlug)
            .NotEmpty()
            .WithMessage("Slug của phòng  không được để trống")
            .MaximumLength(1000)
            .WithMessage("Slug dài tối đa '{MaxLength}'");

            RuleFor(p => p.UrlSlug)
            .MustAsync(async (roomModel, slug, cancellationToken) => !await _roomRepository.IsRoomSlugExistedAsync(roomModel.Id, slug, cancellationToken))
            .WithMessage("Slug '{PropertyValue}' đã được sử dụng");

            RuleFor(p => p.RoomTypeId)
            .NotEmpty()
            .WithMessage("Bạn phải chọn loại phòng cho phòng");

            RuleFor(p => p.VoucherId)
            .NotEmpty()
            .WithMessage("Bạn phải chọn loại giảm giá  của phòng");

            When(p => p.Id <= 0, () =>
            {
                RuleFor(p => p.ImageFile)
                .Must(f => f is { Length: > 0 })
                .WithMessage("Bạn phải chọn hình ảnh cho phòng");
            })
            .Otherwise(() =>
            {
                RuleFor(p => p.ImageFile)
                .MustAsync(SetImageIfNotExist)
                .WithMessage("Bạn phải chọn hình ảnh cho phòng");
            });

        }



        // Kiểm tra xem phòng đã có hình ảnh chưa
        // Nếu chưa có, bắt buộc người dùng phải chọn file
        private async Task<bool> SetImageIfNotExist(RoomEditModel postModel, IFormFile imageFile, CancellationToken cancellationToken)
        {
            // Lấy thông tin bài viết từ CSDL
            var room = await _roomRepository.GetRoomByIdAsync(postModel.Id, cancellationToken);

            // Nếu phòng đã có hình ảnh => Không bắt buộc chọn file
            if (!string.IsNullOrWhiteSpace(room?.Image))
                return true;

            // Ngược lại (phòng chưa có hình ảnh), kiểm tra xem
            // người dùng đã chọn file hay chưa. Nếu chưa thì báo lỗi
            return imageFile is { Length: > 0 };
        }
    }
}
