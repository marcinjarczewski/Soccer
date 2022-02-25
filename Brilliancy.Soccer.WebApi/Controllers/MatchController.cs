using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.WebApi.Models.Match.Read;
using Brilliancy.Soccer.WebApi.Models.Match.Write;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class MatchController : BaseController
    {
        private readonly ITournamentModule _tournamentModule;
        private readonly IMatchModule _matchModule;
        public MatchController(IMapper mapper, ILoginModule loginModule, ITournamentModule tournamentModule, IMatchModule matchNodule) : base(mapper, loginModule)
        {
            _matchModule = matchNodule;
            _tournamentModule = tournamentModule;
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(NewMatchWriteModel model)
        {
            if(model == null)
            {
                throw new InvalidDataException(WebApiTranslations.TournamentController_InvalidTournamentData);
            }
            var dto = _mapper.Map<NewMatchDto>(model);
            int matchId = _matchModule.AddTournamentMatch(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = matchId,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var match = _matchModule.GetMatch(id, this._CurrentUserInfo.Id);
            var model = _mapper.Map<MatchDetailsModel>(match);
            model.Date = System.DateTime.Now;
            return View(model);
        }

        [Authorize]
        [HttpPost("EditCreating")]
        public ActionResult CreatingEdit(CreatingMatchWriteModel model)
        {
            //var match = _matchModule.GetMatch(id, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = 1,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }
    }
}
