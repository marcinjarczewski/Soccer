using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models.Read.Tournament;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class TournamentController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly ITournamentModule _tournamentModule;
        public TournamentController(IMapper mapper, ILoginModule loginModule, ITournamentModule tournamentModule, IApplicationUserManager userManager) : base(mapper, loginModule)
        {
            _userManager = userManager;
            _tournamentModule = tournamentModule;
        }

        public ActionResult Index(string returnUrl = null)
        {
            var t = _tournamentModule.GetTournament(2,1);
            return View();
        }

        [Authorize]
        [Route("Create")]
        public ActionResult Create()
        {
            var model = new NewTournamentModel();
            return View(model);
        }

        [Authorize]
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var dto = _tournamentModule.GetTournament(id, _CurrentUserInfo.Id);
            var model =  _mapper.Map<EditTournamentReadModel>(dto);
            model.EmptyMatch = new Models.Match.Write.NewMatchWriteModel();
            model.EmptyPlayer = new Models.Player.Read.PlayerReadModel();
            model.EmptyUser = new Models.User.Read.UserReadModel();
            return View(model);
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
    }
}
