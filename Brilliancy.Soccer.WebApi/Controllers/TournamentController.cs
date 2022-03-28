using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Common.Helpers.PagedHelper;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Helpers;
using Brilliancy.Soccer.WebApi.Models.Read.Tournament;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Models.User.Write;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class TournamentController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly ITournamentModule _tournamentModule;
        public TournamentController(IMapper mapper, ILoginModule loginModule, ITournamentModule tournamentModule, IApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
            : base(mapper, loginModule, httpContextAccessor)
        {
            _userManager = userManager;
            _tournamentModule = tournamentModule;
        }

        [Authorize]
        [Route("/Tournaments")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("GetList")]
        public PagedResult<TournamentListReadModel> Get([FromQuery] TournamentFilterModel filter, [FromQuery] PagedOptions options)
        {
            var pagedDtos = _tournamentModule.GetTournaments(filter.Term, _CurrentUserInfo.Id, options.Page, options.PerPage);
            var models = _mapper.Map<PagedResult<TournamentListReadModel>>(pagedDtos);
            return models;
        }

        [Authorize]
        [Route("Create")]
        public ActionResult Create()
        {
            var model = new NewTournamentModel();
            return View(model);
        }

        [Authorize]
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            var dto = _tournamentModule.GetTournament(id, _CurrentUserInfo.Id);
            var model = _mapper.Map<TournamentDetailsReadModel>(dto);     
            return AuthorizedResultHelper.AuthorizedResult(View(model), _CurrentUserInfo.TournamentAdmins.Contains(id));
        }

        [Authorize]
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var dto = _tournamentModule.GetTournament(id, _CurrentUserInfo.Id);
            var model = _mapper.Map<EditTournamentReadModel>(dto);
            model.EmptyMatch = new Models.Match.Write.NewMatchWriteModel();
            model.EmptyPlayer = new Models.Player.Read.PlayerReadModel();
            model.EmptyUser = new Models.User.Read.AdminReadModel();
            return AuthorizedResultHelper.AuthorizedResult(View(model), _CurrentUserInfo.TournamentAdmins.Contains(id));
        }


        [Authorize]
        [HttpPost]
        [Route("CreateTournament")]
        public IActionResult CreateTournament(NewTournamentModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new NewTournamentDto();
                _mapper.Map(model, dto);
                dto.OwnerId = this._CurrentUserInfo.Id;
                var newId = this._tournamentModule.AddTournament(dto);

                return new JsonResult(new BaseResultWithDataReadModel
                {
                    IsSuccess = true,
                    Data = newId,
                    Message = WebApiTranslations.TournamentController_Created
                });
            }
            else
            {
                throw new InvalidDataException(WebApiTranslations.TournamentController_InvalidTournamentData);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("EditTournament")]
        public IActionResult EditTournament(EditTournamentWriteModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new TournamentDto();
                _mapper.Map(model, dto);
                this._tournamentModule.EditTournament(dto, this._CurrentUserInfo.Id);

                return new JsonResult(new BaseResultReadModel
                {
                    IsSuccess = true,
                    Message = WebApiTranslations.TournamentController_Edited
                });
            }
            else
            {
                throw new InvalidDataException(WebApiTranslations.TournamentController_InvalidTournamentData);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddAdmin")]
        public IActionResult AddAdmin(AdminWriteModel model)
        {
            if (model == null)
            {
                return new JsonResult(new BaseResultReadModel
                {
                    IsSuccess = false,
                    Message = WebApiTranslations.BaseController_InvalidData
                });
            }
            this._tournamentModule.AddAdmin(model.TournamentId, model.Id, this._CurrentUserInfo.Id);

            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = true,
                Message = WebApiTranslations.TournamentController_AdminAdded
            });
        }

        [Authorize]
        [HttpPost]
        [Route("RemoveAdmin")]
        public IActionResult RemoveAdmin(AdminWriteModel model)
        {
            if (model == null)
            {
                return new JsonResult(new BaseResultReadModel
                {
                    IsSuccess = false,
                    Message = WebApiTranslations.BaseController_InvalidData
                });
            }
            this._tournamentModule.RemoveAdmin(model.TournamentId, model.Id, this._CurrentUserInfo.Id);

            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = true,
                Message = WebApiTranslations.TournamentController_AdminRemoved
            });
        }
    }
}
