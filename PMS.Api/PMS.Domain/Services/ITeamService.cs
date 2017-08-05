using PMS.Domain.Dtos.Team;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.Services
{
    public interface ITeamService
    {
        int Create(TeamDto teamDto);

        TeamDto Get(int id);

        ICollection<TeamDto> GetAll();

        void Update(TeamDto teamDto);

        void Delete(int id);
    }
}
