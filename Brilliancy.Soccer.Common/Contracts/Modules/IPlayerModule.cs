using Brilliancy.Soccer.Common.Dtos.Player;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IPlayerModule
    {
        int AddTournamentPlayer(NewPlayerDto dto, int tournamentId, int userId);

        void RemovePlayerFromTournament(int playerId, int userId);

        void EditPlayers(List<PlayerDto> dto, int matchId, int userId);
    }
}
