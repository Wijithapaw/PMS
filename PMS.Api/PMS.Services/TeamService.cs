using PMS.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using PMS.Domain.Dtos.Team;
using PMS.Data;
using System.Linq;
using PMS.Data.Entities;
using PMS.Utills.PMSExceptions;

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
            var team = new Team
            {
                Name = teamDto.Name,
                Country = teamDto.Country,
                RegisteredDate = teamDto.RegisteredDate
            };

            _dataContext.Teams.Add(team);
            _dataContext.SaveChanges();

            return team.Id;
        }

        public void Delete(int id)
        {
            var team = _dataContext.Teams.Find(id);

            if (team == null)
                throw new RecordNotFoundException("Team", id);

            _dataContext.Teams.Remove(team);
            _dataContext.SaveChanges();
        }

        public TeamDto Get(int id)
        {
            var team = _dataContext.Teams.Where(t => t.Id == id)
                        .Select(t => new TeamDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Country = t.Country,
                            RegisteredDate = t.RegisteredDate
                        }).FirstOrDefault();

            return team;
        }

        public ICollection<TeamDto> GetAll()
        {
            var teams = _dataContext.Teams
                         .Select(t => new TeamDto
                         {
                             Id = t.Id,
                             Name = t.Name,
                             Country = t.Country,
                             RegisteredDate = t.RegisteredDate
                         }).ToArray();

            return teams;
        }

        public void Update(TeamDto teamDto)
        {
            var team = _dataContext.Teams.Find(teamDto.Id);

            if (team == null)
                throw new RecordNotFoundException("Team", teamDto.Id);

            team.Name = teamDto.Name;
            team.Country = teamDto.Country;
            team.RegisteredDate = teamDto.RegisteredDate;

            _dataContext.SaveChanges();
        }
    }
}
