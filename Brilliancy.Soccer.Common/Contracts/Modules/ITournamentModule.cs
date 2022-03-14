using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using PagedList;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface ITournamentModule
    {
        TournamentDto GetTournament(int id, int userId);

        IPagedList<TournamentDto> GetTournaments(string term, int pageNumber, int pageSize);

        int AddTournament(NewTournamentDto dto);

        void EditTournament(TournamentDto dto, int userId);

        void DeleteTournament(int id, int userId);

        void ActivateTournament(int id, int userId);

        void RemoveAdmin(int tournamentId, int adminId, int userId);
    }
}
