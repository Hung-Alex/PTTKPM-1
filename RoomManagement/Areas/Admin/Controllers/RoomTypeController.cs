using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomManagement.Areas.Admin.Models.RoomModel;
using RoomManagement.Areas.Admin.Models.RoomTypeModel;
using RoomManagement.Areas.Admin.Models.VoucherModel;
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
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IPriceManagementRepository _priceManagementRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomEditModel> _validator;
        public RoomTypeController(IRoomTypeRepository roomTypeRepository,
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

        public async Task<IActionResult> Index(

            RoomTypeFilterModel model,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10
            )
        {
            var roomTypeQuery = _mapper.Map<RoomTypeQuery>(model);
            var roomTypeList = await _roomTypeRepository.GetRoomTypesByQuery(roomTypeQuery, new PagingModel() { PageSize = pageSize, PageNumber = pageNumber }, roomTypes => roomTypes.ProjectToType<RoomTypeDto>());

            ViewBag.RoomTypeList = new PaginationResult<RoomTypeDto>(roomTypeList);
            ViewData["Query"] = model;
            return View("Index", model);




           

        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromServices]
             IValidator<RoomTypeEditModel> validator, RoomTypeEditModel model)
        {

            //slug null
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
            }
            if (!ModelState.IsValid)
            {
                //await PopulatePostsEditModelAsync(model);
                return View(model);
            }

            var roomType = model.Id > 0
              ? await _roomTypeRepository.GetRoomTypeByIdAsync(model.Id)
              : null;

            if (roomType == null)
            {
                roomType = _mapper.Map<RoomType>(model);
                roomType.Id = 0;

            }
            else
            {
                _mapper.Map(model, roomType);

            }
            await _roomTypeRepository.AddOrUpdateRoomType(roomType);
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var roomType = id > 0
           ? await _roomTypeRepository.GetRoomTypeByIdAsync(id)
           : null;

            var model = roomType == null
            ? new RoomTypeEditModel()
            : _mapper.Map<RoomTypeEditModel>(roomType);

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomTypeQuery = await _roomTypeRepository.DeleteRoomType(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> VerifyRoomTypeSlug(int id, string urlSlug)
        {
            var slugExisted = await _roomTypeRepository.IsRoomTypeSlugExistedAsync(id, urlSlug);
            return slugExisted ? Json($"slug '{urlSlug}'  đã được sử dụng") : Json(true);
        }

        public async Task PopulateRoomTypesFitlterModelAsync(RoomTypeFilterModel model)
        {
            var roomTypes = await _roomTypeRepository.GetAllAsync();
            model.RoomTypeList = roomTypes.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
        }
    }
}
