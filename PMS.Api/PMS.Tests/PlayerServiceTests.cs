using Microsoft.EntityFrameworkCore;
using PMS.Data;
using System;
using System.Collections.Generic;
using System.Text;
using PMS.Data.Entities;
using PMS.Services;
using Xunit;
using PMS.Domain.Dtos.Player;
using PMS.Utills.PMSExceptions;

namespace PMS.Tests
{
    public class PlayerServiceTests
    {
        public class Create
        {
            [Theory]
            [InlineData("Lakshan", "Sandakan", "sandakan@gmail.com", "Sri Lanka", "1995-1-2")]
            [InlineData("Angelo", "Matthews", "angelo@gmail.com", "Sri Lanka", "1989-1-10")]
            [InlineData("Niroshan", "Dickwalla", "dickwalla@gmail.com", "Sri Lanka", "1978-1-2")]
            [InlineData("KL", "Rahul", "rahul@gmail.com", "India", "1993-12-2")]
            public void WhenPassingCorrectData_CreatesSucessfully(string firstName, string lastName, string email, string homeCountry, DateTime birthday)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var player = CreatePlayerDto(0, firstName, lastName, email, homeCountry, birthday);

                    var id = service.Create(player);

                    Assert.True(id > 0);

                    var newPlayer = service.Get(id);

                    string errorMsg = string.Empty;
                    Assert.True(ValidatePlayer(newPlayer, id, firstName, lastName, email, homeCountry, birthday, ref errorMsg), errorMsg);
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
            public void WhenPlayerExists_DeleteSuccessfully(int id)
            {

                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    service.Delete(id);

                    var deletedPlayer = service.Get(id);

                    Assert.Null(deletedPlayer);
                }
            }

            [Theory]
            [InlineData(100)]
            [InlineData(200)]
            [InlineData(300)]
            [InlineData(400)]
            public void WhenPlayerNotExists_ThrowsException(int id)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    Assert.Throws<RecordNotFoundException>(() => service.Delete(id));
                }
            }
        }

        public class Get
        {
            [Theory]
            [InlineData(1, "Sachin", "Tendulkar", "sachin@gmail.com", "India", "1975-1-2")]
            [InlineData(2, "Virat", "Kohli", "virat@gmail.com", "India", "1989-1-10")]
            [InlineData(4, "Kumar", "Sangakkara", "sanga@gmail.com", "Sri Lanka", "1978-1-2")]
            [InlineData(7, "Ben", "Stokes", "ben@gmail.com", "England", "1993-1-2")]
            public void WhenPlayerExists_ReturnPlayer(int id, string firstName, string lastName, string email, string homeCountry, DateTime birthday)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var player = service.Get(id);
                    string errorMsg = string.Empty;
                    Assert.True(ValidatePlayer(player, id, firstName, lastName, email, homeCountry, birthday, ref errorMsg), errorMsg);
                }
            }

            [Theory]
            [InlineData(20)]
            [InlineData(100)]
            [InlineData(201)]
            [InlineData(1000)]
            public void WhenPlayerNotExists_ReturnNull(int id)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var player = service.Get(id);
                   
                    Assert.Null(player);
                }
            }
        }

        public class GetAll
        {
            [Fact]
            public void WhenPlayersExist_ReturnsAll()
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var players = service.GetAll();

                    Assert.Equal(10, players.Count);
                }
            }

            [Fact]
            public void WhenPlayersNotExist_ReturnsNon()
            {
                var options = Helper.GetContextOptions();

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var players = service.GetAll();

                    Assert.Equal(0, players.Count);
                }
            }
        }

        public class Update
        {
            [Theory]
            [InlineData(1, "Sachin", "Tendulkar", "sachin@gmail.com", "India", "1970-10-2")]
            [InlineData(2, "Virat", "Kohli", "kohli@gmail.com", "India", "1989-1-10")]
            [InlineData(4, "Kumara", "Sangakkara", "sanga@gmail.com", "Sri Lanka", "1978-1-2")]
            [InlineData(7, "Benson", "Stokes", "ben@gmail.com", "England", "1992-1-2")]
            public void WhenPlayerExists_UpdatesSuccessfully(int id, string firstName, string lastName, string email, string homeCountry, DateTime birthday)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var player = CreatePlayerDto(id, firstName, lastName, email, homeCountry, birthday);

                    service.Update(player);

                    var updatedPlayer = service.Get(id);

                    string errorMsg = string.Empty;
                    Assert.True(ValidatePlayer(updatedPlayer, id, firstName, lastName, email, homeCountry, birthday, ref errorMsg), errorMsg);
                }
            }

            [Theory]
            [InlineData(100, "Sachin", "Tendulkar", "sachin@gmail.com", "India", "1970-10-2")]
            [InlineData(200, "Virat", "Kohli", "kohli@gmail.com", "India", "1989-1-10")]
            [InlineData(400, "Kumara", "Sangakkara", "sanga@gmail.com", "Sri Lanka", "1978-1-2")]
            [InlineData(70, "Benson", "Stokes", "ben@gmail.com", "England", "1992-1-2")]
            public void WhenPlayerNotExists_ThrowsException(int id, string firstName, string lastName, string email, string homeCountry, DateTime birthday)
            {
                var options = Helper.GetContextOptions();

                SetupTestData(options);

                using (var context = new DataContext(options))
                {
                    var service = new PlayerService(context);

                    var player = CreatePlayerDto(id, firstName, lastName, email, homeCountry, birthday);

                    Assert.Throws<RecordNotFoundException>(() => service.Update(player));
                }
            }
        }

        #region Helper Methods

        private static void SetupTestData(DbContextOptions<DataContext> options)
        {
            using (var context = new DataContext(options))
            {
                context.Players.AddRange(GetPlayers());
                context.SaveChanges();
            }
        }

        public static Player[] GetPlayers()
        {
            var players = new Player[]
                {
                    CreatePlayer(1, "Sachin", "Tendulkar", "sachin@gmail.com", "India", new DateTime(1975, 1, 2)),
                    CreatePlayer(2, "Virat", "Kohli", "virat@gmail.com", "India", new DateTime(1989, 1, 10)),
                    CreatePlayer(3, "Umar", "Akmal", "umar@gmail.com", "Pakistan", new DateTime(1985, 10, 2)),
                    CreatePlayer(4, "Kumar", "Sangakkara", "sanga@gmail.com", "Sri Lanka", new DateTime(1978, 1, 2)),
                    CreatePlayer(5, "Lasith", "Malinga", "lasith@gmail.com", "Sri Lanka", new DateTime(1984, 4, 2)),
                    CreatePlayer(6, "Mahela", "Jayawardane", "mahela@gmail.com", "Sri Lanka", new DateTime(1978, 1, 2)),
                    CreatePlayer(7, "Ben", "Stokes", "ben@gmail.com", "England", new DateTime(1993, 1, 2)),
                    CreatePlayer(8, "Kusal", "Mendis", "kusal@gmail.com", "Sri Lanka", new DateTime(1995, 1, 2)),
                    CreatePlayer(9, "Josh", "Butler", "josh@gmail.com", "England", new DateTime(1996, 9, 2)),
                    CreatePlayer(10, "Steve", "Smith", "steve@gmail.com", "Australia", new DateTime(1992, 8, 1))
                };
            return players;
        }

        private static Player CreatePlayer(int id, string firstName, string lastName, string email, string homeCountry, DateTime birthday)
        {
            var player = new Player
            {
                //Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                HomeCountry = homeCountry,
                Birthday = birthday
            };

            return player;
        }


        private static PlayerDto CreatePlayerDto(int id, string firstName, string lastName, string email, string homeCountry, DateTime birthday)
        {
            var player = new PlayerDto
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                HomeCountry = homeCountry,
                Birthday = birthday
            };

            return player;
        }

        private static bool ValidatePlayer(PlayerDto player, int id, string firstName, string lastName, string email, string homeCountry, DateTime birthday, ref string errorMsg)
        {
            errorMsg = string.Empty;

            if (player == null)
            {
                errorMsg = "Player is null";
            }
            else
            {
                if (!player.Id.Equals(id))                
                    errorMsg += "ID does not match. Expected: " + id + " Actual: " + player.Id;

                if (!player.FirstName.Equals(firstName))
                    errorMsg += "\nFirst Name does not match. Expected: " + firstName + " Actual: " + player.FirstName;
                
                if (!player.LastName.Equals(lastName))
                    errorMsg += "\nLast Name does not match. Expected: " + lastName + " Actual: " + player.LastName;
                
                if (!player.Email.Equals(email))
                    errorMsg += "\nEmail does not match. Expected: " + email + " Actual: " + player.Email;

                if (!player.HomeCountry.Equals(homeCountry))
                    errorMsg += "\nHome Country does not match. Expected: " + homeCountry + " Actual: " + player.HomeCountry;
                
                if (!player.Birthday.Equals(birthday))
                    errorMsg += "\nBirthday does not match. Expected: " + birthday + " Actual: " + player.Birthday;
            }
            return (errorMsg == string.Empty);
        }

        #endregion
    }
}
