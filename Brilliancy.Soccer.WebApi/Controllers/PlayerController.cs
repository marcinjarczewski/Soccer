using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models.Player.Write;
using Brilliancy.Soccer.WebApi.Models.Read.Tournament;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class PlayerController : BaseController
    {
        private readonly ITournamentModule _tournamentModule;
        private readonly IPlayerModule _playerModule;
        public PlayerController(IMapper mapper, ILoginModule loginModule, ITournamentModule tournamentModule, IPlayerModule playerModule) : base(mapper, loginModule)
        {
            _playerModule = playerModule;
            _tournamentModule = tournamentModule;
        }

        [Authorize]
        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(PlayerListWriteModel model)
        {
            if(model == null)
            {
                throw new InvalidDataException(WebApiTranslations.TournamentController_InvalidTournamentData);
            }
            var playersDto = _mapper.Map<List<PlayerDto>>(model.Players);
             _playerModule.EditPlayers(playersDto, model.TournamentId, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = true,
                Message = WebApiTranslations.RegisterSuccessful
            });
        }
    }
}
