using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using PagedList;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IPlayerModule
    {
        int AddTournamentPlayer(NewPlayerDto dto, int tournamentId, int userId);

        void RemovePlayerFromTournament(int playerId, int userId);
    }
}
