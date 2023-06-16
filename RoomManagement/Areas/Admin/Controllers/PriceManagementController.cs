using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomManagement.Areas.Admin.Models.PriceManagementModel;
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
    public class PriceManagementController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IPriceManagementRepository _priceManagementRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomEditModel> _validator;
        public PriceManagementController(IRoomTypeRepository roomTypeRepository,
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

            PriceManagementFilterModel model,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10
            )
        {
            var priceManagementQuery = _mapper.Map<PriceManagementQuery>(model);
            var priceManagementList = await _priceManagementRepository.GetPriceManagementsByQuery(priceManagementQuery, new PagingModel() { PageSize = pageSize, PageNumber = pageNumber }, priceManagements => priceManagements.ProjectToType<PriceManagementDto>());

            ViewBag.PageType = "Giá phòng";

            ViewBag.PriceManagementList = new PaginationResult<PriceManagementDto>(priceManagementList);
            ViewData["Query"] = model;
            return View("Index", model);

        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromServices]
             IValidator<PriceManagementEditModel> voucherValidator, PriceManagementEditModel model)
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

            var priceManament = model.Id > 0
              ? await _priceManagementRepository.GetPriceManagementByIdAsync(model.Id)
              : null;

            if (priceManament == null)
            {
                priceManament = _mapper.Map<PriceManagement>(model);
                priceManament.Id = 0;

            }
            else
            {
                _mapper.Map(model, priceManament);

            }




            await _priceManagementRepository.AddOrUpdatePriceManagement(priceManament);


            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var priceManagement = id > 0
           ? await _priceManagementRepository.GetPriceManagementByIdAsync(id)
           : null;

            var model = priceManagement == null
            ? new PriceManagementEditModel()
            : _mapper.Map<PriceManagementEditModel>(priceManagement);

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeletePriceManagement(int id)
        {
            var PriceManagementQuey = await _priceManagementRepository.DeletePriceManagement(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> VerifyPriceManagementSlug(int id, string urlSlug)
        {
            var slugExisted = await _priceManagementRepository.IsPriceManagementSlugExistedAsync(id, urlSlug);
            return slugExisted ? Json($"slug '{urlSlug}'  đã được sử dụng") : Json(true);
        }

        public async Task PopulatePriceManagementsFitlterModelAsync(PriceManagementFilterModel model)
        {
            var priceManagements = await _priceManagementRepository.GetAllAsync();
            model.PriceManagementList = priceManagements.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
        }
    }
}
