using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using RoomManagement.Areas.Admin.Models.RoomModel;
using RoomManagement.Core.Collections;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using RoomManagement.Models;
using RoomManagement.Services.Media;
using RoomManagement.Services.RoomMangementService.PriceManagementSerivce;
using RoomManagement.Services.RoomMangementService.RoomSerivce;
using RoomManagement.Services.RoomMangementService.RoomTypeSerivce;
using RoomManagement.Services.RoomMangementService.VoucherSerivce;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class RoomController : Controller
    {
        
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IPriceManagementRepository _priceManagementRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomEditModel> _validator;
        public RoomController(IRoomTypeRepository roomTypeRepository, 
            IRoomRepository roomRepository,
            IVoucherRepository voucherRepository,
            IPriceManagementRepository priceManagementRepository,
            IMediaManager mediaManager,
            IMapper mapper,
            IValidator<RoomEditModel> validator
            )
        {           
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
            _voucherRepository = voucherRepository;
            _priceManagementRepository = priceManagementRepository;
            _mediaManager = mediaManager;
            _mapper = mapper;
            _validator = validator;

        }
        public async Task<IActionResult> Index(RoomFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 10)
        {
            var roomQuery = _mapper.Map<RoomQuery>(model);
            var roomList = await _roomRepository.GetRoomsByQuery(roomQuery, new PagingModel() {PageSize=pageSize,PageNumber=pageNumber }, rooms => rooms.ProjectToType<RoomDto>());
            
            var paginationResult = new PaginationResult<RoomDto>(roomList);
            await PopulateRoomFilterModelAsync(model);

            return View("Index", paginationResult);
        }

        private async Task PopulateRoomFilterModelAsync(RoomFilterModel model)
        {
            var priceManagements = await _priceManagementRepository.GetAllAsync();
            var vouchers = await _voucherRepository.GetAllAsync();
            var roomTypes = await _roomTypeRepository.GetAllAsync();


            model.PriceManagementList = priceManagements.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });

            model.VoucherList = vouchers.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            model.RoomTypeList = roomTypes.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        private async Task PopulateRoomEditModelAsync(RoomEditModel model)
        {
            var priceManagements = await _priceManagementRepository.GetAllAsync();
            var vouchers = await _voucherRepository.GetAllAsync();
            var roomTypes = await _roomTypeRepository.GetAllAsync();

            model.PriceManagementList = priceManagements.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });

            model.VoucherList = vouchers.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            model.RoomTypeList = roomTypes.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0)
        {
            // ID = 0 -> Thêm room mới
            // ID > 0 : Đọc dữ liệu của bài viết từ CSDL
            var room = id > 0 ? await _roomRepository.GetRoomByIdAsync(id) : null;

            // Tạo view model từ dữ liệu của bài viết
            var model = room == null ? new RoomEditModel() : _mapper.Map<RoomEditModel>(room);

            // Gán các giá trị khác nhau cho view model
            await PopulateRoomEditModelAsync(model);

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoomEditModel model)
        {
            
            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
            }

            if (!ModelState.IsValid)
            {
                await PopulateRoomEditModelAsync(model);
                return View(model);
            }

            var room = model.Id > 0 ? await _roomRepository.GetRoomByIdAsync(model.Id) : null;

            if (room == null)
            {
                room = _mapper.Map<Room>(model);

                room.Id = 0;
                
            }
            else
            {
                _mapper.Map(model, room);

                
            }

            // Nếu người dùng có upload hình ảnh minh họa cho bài viết
            if (model.ImageFile?.Length > 0)
            {
                // Thực hiện việc lưu tập tin vào thư mực uploads
                var newImagePath = await _mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);

                // Nếu lưu thành công, xóa tập tin hình ảnh cũ (nếu có)
                if (!string.IsNullOrWhiteSpace(newImagePath))
                {
                    await _mediaManager.DeleteFileAsync(room.Image);
                    room.Image = newImagePath;
                }
            }

            await _roomRepository.AddOrUpdateRoom(room);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<ActionResult> VerifyRoomSlug(int id, string urlSlug)
        {
            var slugExisted = await _roomRepository.IsRoomSlugExistedAsync(id, urlSlug);

            return slugExisted ? Json($"Slug '{urlSlug}' đã được sử dụng") : Json(true);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRoom(string id)
        {
            await _roomRepository.DeleteRoom(Convert.ToInt32(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
