using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomManagement.Areas.Admin.Models.RoomModel;
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
    public class VoucherController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IPriceManagementRepository _priceManagementRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomEditModel> _validator;
        public VoucherController(IRoomTypeRepository roomTypeRepository,
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

            VoucherFilterModel model,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10
            )
        {
            var voucherQuery = _mapper.Map<VoucherQuery>(model);
            var voucherList = await _voucherRepository.GetVouchersByQuery(voucherQuery, new PagingModel() { PageSize = pageSize, PageNumber = pageNumber }, vouchers => vouchers.ProjectToType<VoucherDto>());

            ViewBag.PageType = "Voucher";

            ViewBag.VoucherList = new PaginationResult<VoucherDto>(voucherList);
            ViewData["Query"] = model;
            return View("Index", model);           
     
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromServices]
             IValidator<VoucherEditModel> voucherValidator, VoucherEditModel model)
        {

            //slug null
            var validationResult = await voucherValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
            }
            if (!ModelState.IsValid)
            {
                //await PopulatePostsEditModelAsync(model);
                return View(model);
            }

            var voucher = model.Id > 0
              ? await _voucherRepository.GetVoucherByIdAsync(model.Id)
              : null;

            if (voucher == null)
            {
                voucher = _mapper.Map<Voucher>(model);
                voucher.Id = 0;

            }
            else
            {
                _mapper.Map(model, voucher);

            }

            await _voucherRepository.AddOrUpdateVoucher(voucher);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var voucher = id > 0
           ? await _voucherRepository.GetVoucherByIdAsync(id)
           : null;

            var model = voucher == null
            ? new VoucherEditModel()
            : _mapper.Map<VoucherEditModel>(voucher);

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var voucherQuery = await _voucherRepository.DeleteVoucher(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> VerifyVoucherSlug(int id, string urlSlug)
        {
            var slugExisted = await _voucherRepository.IsVoucherSlugExistedAsync(id, urlSlug);
            return slugExisted ? Json($"slug '{urlSlug}'  đã được sử dụng") : Json(true);
        }

        public async Task PopulateVouchersFitlterModelAsync(VoucherFilterModel model)
        {
            var vouchers = await _voucherRepository.GetAllAsync();
            model.VoucherList = vouchers.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
        }

    }
}
