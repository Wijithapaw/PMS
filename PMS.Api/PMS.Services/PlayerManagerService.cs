using PMS.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using PMS.Domain.Dtos.Player;
using PMS.Data;
using System.Linq;

namespace PMS.Services
{
    public class PlayerManagerService : IPlayerManagerServicecs
    {
        private DataContext _dataContext;

        public PlayerManagerService(DataContext context)
        {
            _dataContext = context;
        }

        public int AddPlayerToTeam(PlayerTeamDto playerTeam)
        {
            throw new NotImplementedException();
        }

        public ICollection<PlayerDto> GetTeamPlayersForTheDate(int teamId, DateTime date)
        {
            var players = _dataContext.PlayerTeams
                        .Where(pt => pt.TeamId == teamId
                                && pt.StartDate <= date
                                && (pt.EndDate == null || pt.EndDate >= date))
                         .Select(p => new PlayerDto
                         {
                             Id = p.Player.Id,
                             FirstName = p.Player.FirstName,
                             LastName = p.Player.LastName,
                             Email = p.Player.Email,
                             Birthday = p.Player.Birthday,
                             HomeCountry = p.Player.HomeCountry
                         }).ToArray();

            return players;                         
        }

        public void RemovePlayerFromTeam(PlayerTeamDto playerTeam)
        {
            throw new NotImplementedException();
        }
    }
}
