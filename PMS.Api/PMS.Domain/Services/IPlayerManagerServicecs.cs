using PMS.Domain.Dtos.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.Services
{
    public interface IPlayerManagerServicecs
    {
        int AddPlayerToTeam(PlayerTeamDto playerTeam);

        ICollection<PlayerDto> GetTeamPlayersForTheDate(int teamId, DateTime date);

        void RemovePlayerFromTeam(PlayerTeamDto playerTeam);
    }
}
