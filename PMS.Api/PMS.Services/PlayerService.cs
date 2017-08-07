using PMS.Domain.Services;
using System;
using PMS.Domain.Dtos.Player;
using System.Collections.Generic;
using PMS.Data;
using System.Linq;
using PMS.Data.Entities;
using PMS.Utills.PMSExceptions;

namespace PMS.Services
{
    public class PlayerService : IPlayerService
    {
        private DataContext _dataContext;

        public PlayerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int Create(PlayerDto playerDto)
        {
            var player = new Player
            {
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                Email = playerDto.Email,
                HomeCountry = playerDto.HomeCountry,
                Birthday = playerDto.Birthday,
            };

            _dataContext.Players.Add(player);
            _dataContext.SaveChanges();

            return player.Id;
        }

        public void Delete(int id)
        {
            var player = _dataContext.Players.Find(id);

            if (player == null)
                throw new RecordNotFoundException("Player", id);

            _dataContext.Players.Remove(player);
            _dataContext.SaveChanges();
        }

        public PlayerDto Get(int id)
        {
            var player = _dataContext.Players
                            .Where(p => p.Id == id)
                            .Select(p => new PlayerDto
                            {
                                Id = p.Id,
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                Email = p.Email,
                                HomeCountry = p.HomeCountry,
                                Birthday = p.Birthday
                            }).FirstOrDefault();

            return player;
        }

        public ICollection<PlayerDto> GetAll()
        {
            var players = _dataContext.Players
                            .Select(p => new PlayerDto
                            {
                                Id = p.Id,
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                Email = p.Email,
                                HomeCountry = p.HomeCountry,
                                Birthday = p.Birthday
                            }).ToArray();

            return players;
        }

        public void Update(PlayerDto playerDto)
        {
            var player = _dataContext.Players.Find(playerDto.Id);

            if (player == null)
                throw new RecordNotFoundException("Player", playerDto.Id);

            player.FirstName = playerDto.FirstName;
            player.LastName = playerDto.LastName;
            player.Email = playerDto.Email;
            player.Birthday = playerDto.Birthday;
            player.HomeCountry = playerDto.HomeCountry;
            
            _dataContext.SaveChanges();
        }
    }
}
