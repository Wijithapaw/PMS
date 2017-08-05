using PMS.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using PMS.Domain.Dtos.Team;
using PMS.Data;

namespace PMS.Services
{
    public class TeamService : ITeamService
    {
        private DataContext _dataContext;

        public TeamService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int Create(TeamDto teamDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TeamDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TeamDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TeamDto teamDto)
        {
            throw new NotImplementedException();
        }
    }
}
