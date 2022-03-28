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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class MatchController : BaseController
    {
        private readonly ITournamentModule _tournamentModule;
        private readonly IMatchModule _matchModule;
        public MatchController(IMapper mapper, ILoginModule loginModule, ITournamentModule tournamentModule, IMatchModule matchModule, IHttpContextAccessor httpContextAccessor)
            : base(mapper, loginModule, httpContextAccessor)
        {
            _matchModule = matchModule;
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
            model.EmptyGoal = new GoalReadModel();
            return View(model);
        }

        [Authorize]
        [HttpGet("LiveEdit/{id}")]
        public ActionResult LiveEdit(int id)
        {
            var match = _matchModule.GetMatch(id, this._CurrentUserInfo.Id);
            var model = _mapper.Map<MatchDetailsModel>(match);
            model.EmptyGoal = new GoalReadModel();
            return View(model);
        }

        [Authorize]
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            var match = _matchModule.GetMatch(id, this._CurrentUserInfo.Id);
            var model = _mapper.Map<MatchDetailsModel>(match);
            return View(model);
        }


        [Authorize]
        [HttpGet("LiveEditModel/{id}")]
        public ActionResult LiveEditModel(int id)
        {
            var match = _matchModule.GetMatch(id, this._CurrentUserInfo.Id);
            var model = _mapper.Map<MatchDetailsModel>(match);
            model.EmptyGoal = new GoalReadModel();
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model
            });
        }


        [Authorize]
        [HttpPost("ChangeToPending")]
        public ActionResult ChangeToPending(MatchChangeStateWriteModel model)
        {
            _matchModule.ChangeMatchStateToPending(model?.Id ?? 0, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("ChangeToCreating")]
        public ActionResult ChangeToCreating(MatchChangeStateWriteModel model)
        {
            _matchModule.ChangeMatchStateToCreating(model?.Id ?? 0, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("AddGoal")]
        public ActionResult AddGoal(MatchOngoingEditModel model)
        {
            var dto = _mapper.Map<MatchOngoingEditDto>(model);
            _matchModule.AddGoal(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("RemoveGoal")]
        public ActionResult RemoveGoal(MatchOngoingEditModel model)
        {
            var dto = _mapper.Map<MatchOngoingEditDto>(model);
            _matchModule.RemoveGoal(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("ChangeToOngoing")]
        public ActionResult ChangeToOngoing(MatchChangeStateWriteModel model)
        {
            _matchModule.ChangeMatchStateToOngoing(model?.Id ?? 0, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("ChangeToCanceled")]
        public ActionResult ChangeToCanceled(MatchChangeStateWriteModel model)
        {
            _matchModule.ChangeMatchStateToCanceled(model?.Id ?? 0, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("ChangeToFinished")]
        public ActionResult ChangeToFinished(MatchChangeStateWriteModel model)
        {
            _matchModule.ChangeMatchStateToFinished(model?.Id ?? 0, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = model?.Id,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }

        [Authorize]
        [HttpPost("EditCreating")]
        public ActionResult CreatingEdit(CreatingMatchWriteModel model)
        {
            var dto = _mapper.Map<MatchCreatingEditDto>(model);
            var match = _matchModule.EditCreatingMatch(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                Data = match,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }


        [Authorize]
        [HttpPost("EditPending")]
        public ActionResult EditPending(PendingMatchWriteModel model)
        {
            var dto = _mapper.Map<MatchPendingEditDto>(model);
            _matchModule.EditGoals(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultWithDataReadModel
            {
                IsSuccess = true,
                //Data = match,
                Message = WebApiTranslations.MatchController_AddSuccess
            });
        }
    }
}
