using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMS.Data;
using PMS.Data.Entities;
using PMS.Domain.Dtos.Team;
using PMS.Services;
using PMS.Utills.PMSExceptions;
using System;
using Xunit;

namespace PMS.Tests
{
    public class TeamServiceTests
    {
        public class Create
        {
            [Theory]
            [InlineData("Lahore Lions", "Pakistan", "2015-5-5")]
            [InlineData("Sydney Thunders", "Australia", "2012-2-6")]
            [InlineData("Australia Cricket", "Australia", "1935-7-7")]
            public void WhenPassingCorrectData_CreateSuccessfully(string name, string country, DateTime regDate)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var teamDto = new TeamDto
                    {
                        Name = name,
                        Country = country,
                        RegisteredDate = regDate
                    };

                    var id = service.Create(teamDto);

                    var team = service.Get(id);

                    Assert.NotNull(team);
                    Assert.Equal(name, team.Name);
                    Assert.Equal(country, team.Country);
                    Assert.Equal(regDate, team.RegisteredDate);
                }
            }
        }

        public class Delete
        {
            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            public void WhenTeamExists_DeleteSuccessfully(int id)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    service.Delete(id);

                    var deletedPlayer = service.Get(id);

                    Assert.Null(deletedPlayer);                  
                }
            }


            [Theory]
            [InlineData(10)]
            [InlineData(12)]
            [InlineData(13)]
            [InlineData(14)]
            public void WhenTeamNotExists_ThrowsException(int id)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    Assert.Throws<RecordNotFoundException>(() => service.Delete(id));
                }
            }
        }

        public class Get
        {

            [Theory]
            [InlineData(1, "Sri Lanka Cricket", "Sri Lanka", "1980-1-1")]
            [InlineData(2, "India Cricket", "India", "1950-2-1")]
            [InlineData(5, "Surrey", "England", "1955-12-1")]
            public void WhenTeamExists_ReturnTeam(int id, string name, string country, DateTime regDate)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var team = service.Get(id);

                    Assert.NotNull(team);
                    Assert.Equal(name, team.Name);
                    Assert.Equal(country, team.Country);
                    Assert.Equal(regDate, team.RegisteredDate);
                }                    
            }

            [Theory]
            [InlineData(10)]
            [InlineData(11)]
            [InlineData(12)]
            public void WhenTeamNotExists_ReturnNull(int id)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var team = service.Get(id);

                    Assert.Null(team);                   
                }
            }
        }

        public class GetAll
        {
            [Fact]
            public void WhenTeamsExists_ReturnAll()
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var teams = service.GetAll();

                    Assert.Equal(5, teams.Count);
                }
            }

            [Fact]
            public void WhenTeamsNotExists_ReturnAll()
            {
                var options = Helper.GetContextOptions();

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var teams = service.GetAll();

                    Assert.Equal(0, teams.Count);
                }
            }
        }

        public class Update
        {
            [Theory]
            [InlineData(1, "Sri Lanka National Cricket", "Sri Lanka","1975-5-6")]
            [InlineData(3, "Pakistan National Cricket", "Republic of Pakistan", "1970-5-1")]
            [InlineData(2, "India National Cricket", "Republic of India", "1890-11-30")]
            public void WhenTeamExists_UpdateSucessfully(int id, string name, string country, DateTime regDate)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var teamDto = new TeamDto
                    {
                        Id = id,
                        Name = name,
                        Country = country,
                        RegisteredDate = regDate
                    };

                    service.Update(teamDto);

                    var team = service.Get(id);

                    Assert.NotNull(team);
                    Assert.Equal(name, team.Name);
                    Assert.Equal(country, team.Country);
                    Assert.Equal(regDate, team.RegisteredDate);
                }
            }


            [Theory]
            [InlineData(10, "Sri Lanka National Cricket", "Sri Lanka", "1975-5-6")]
            [InlineData(11, "Pakistan National Cricket", "Republic of Pakistan", "1970-5-1")]
            [InlineData(12, "India National Cricket", "Republic of India", "1890-11-30")]
            public void WhenTeamNotExists_ThrowsException(int id, string name, string country, DateTime regDate)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (DataContext context = new DataContext(options))
                {
                    var service = new TeamService(context);

                    var teamDto = new TeamDto
                    {
                        Id = id,
                        Name = name,
                        Country = country,
                        RegisteredDate = regDate
                    };

                    Assert.Throws<RecordNotFoundException>(() => service.Update(teamDto));                  
                }
            }
        }

        private static void SetupTestData(DbContextOptions<DataContext> options)
        {
            using (var context = new DataContext(options))
            {
                context.Teams.AddRange(GetTeams());
                context.SaveChanges();
            }
        }

        public static Team[] GetTeams()
        {
            var teams = new Team[]
            {
                CreateTeam(1, "Sri Lanka Cricket", "Sri Lanka", new DateTime(1980, 1, 1)),
                CreateTeam(2, "India Cricket", "India", new DateTime(1950, 2, 1)),
                CreateTeam(3, "Pakistan Cricket", "Pakistan", new DateTime(1960, 3, 5)),
                CreateTeam(4, "Mumbai Indians", "India", new DateTime(2007, 3, 6)),
                CreateTeam(5, "Surrey", "England", new DateTime(1955, 12, 1))
            };

            return teams;
        }

        private static Team CreateTeam(int id, string name, string country, DateTime regDate)
        {
            var team = new Team
            {
                //Id = id,
                Name = name,
                Country = country,
                RegisteredDate = regDate
            };
            return team;
        }
    }
}
