using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Helpers.PagedHelper;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface ITournamentModule
    {
        TournamentDto GetTournament(int id, int userId);

        PagedResult<TournamentDto> GetTournaments(string term, int userId, int pageNumber, int pageSize);

        int AddTournament(NewTournamentDto dto);

        void EditTournament(TournamentDto dto, int userId);

        void DeleteTournament(int id, int userId);

        void ActivateTournament(int id, int userId);

        void AddAdmin(int tournamentId, int adminId, int userId);

        void RemoveAdmin(int tournamentId, int adminId, int userId);
    }
}
