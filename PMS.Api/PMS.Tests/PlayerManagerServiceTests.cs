using Microsoft.EntityFrameworkCore;
using PMS.Data;
using System;
using System.Collections.Generic;
using System.Text;
using PMS.Data.Entities;
using PMS.Services;
using Xunit;

namespace PMS.Tests
{
    public class PlayerManagerServiceTests
    {
        public class GetTeamPlayersForTheDate
        {
            [Theory]
            [InlineData(1, "2017-8-7", 2)]
            [InlineData(1, "2016-1-1", 3)]
            [InlineData(1, "2015-12-31", 2)]
            [InlineData(2, "2017-8-7", 1)]
            [InlineData(2, "2010-8-1", 2)]
            public void WhenPlayersExists_ReturnsOnlyValidPlayers(int teamId, DateTime date, int expectedCount)
            {
                using (var connection = Helper.GetSqliteConnection())
                {
                    var options = Helper.GetSqliteContextOptions(connection);

                    SetupTestData(options);

                    using (var context = new DataContext(options))
                    {
                        var service = new PlayerManagerService(context);

                        var players = service.GetTeamPlayersForTheDate(teamId, date);

                        Assert.Equal(expectedCount, players.Count);
                    }
                }
            }
        }

        #region Helper Methods

        private static void SetupTestData(DbContextOptions<DataContext> options)
        {
            using (var context = new DataContext(options))
            {
                context.Teams.AddRange(TeamServiceTests.GetTeams());
                context.Players.AddRange(PlayerServiceTests.GetPlayers());

                context.SaveChanges();

                context.PlayerTeams.AddRange(GetPlayerTeams());

                context.SaveChanges();
            }  
        }

        public static PlayerTeam[] GetPlayerTeams()
        {
            var playerTeams = new PlayerTeam[]
                {
                    CreatePlayerTeam(2, 1, new DateTime(1990, 1, 1), new DateTime(2012, 12, 31)), //Sachin
                    CreatePlayerTeam(4, 1, new DateTime(2007, 3, 1), new DateTime(2015, 5, 31)),
                    CreatePlayerTeam(5, 1, new DateTime(2000, 2, 1), new DateTime(2005, 5, 31)),

                    CreatePlayerTeam(1, 4, new DateTime(2000, 1, 1), new DateTime(2016, 12, 31)), //Sanga
                    CreatePlayerTeam(5, 4, new DateTime(2016, 4, 1), null),

                    CreatePlayerTeam(1, 5, new DateTime(2004, 1, 1), null), //Malinga
                    CreatePlayerTeam(4, 5, new DateTime(2007, 1, 1), null),

                    CreatePlayerTeam(2, 2, new DateTime(2009, 3, 1), null), //Virat

                    CreatePlayerTeam(1, 8, new DateTime(2016, 1, 1), null) //Kusal
                };

            return playerTeams;
        }

        private static PlayerTeam CreatePlayerTeam(int teamId, int playerId, DateTime startDate, DateTime? endDate)
        {
            var player = new PlayerTeam
            {
                TeamId = teamId,
                PlayerId = playerId,
                StartDate = startDate,
                EndDate = endDate,
            };

            return player;
        }

        #endregion

    }
}
