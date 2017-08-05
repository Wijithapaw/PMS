using PMS.Domain.Services;
using System;
using PMS.Domain.Dtos.Player;
using System.Collections.Generic;
using PMS.Data;

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PlayerDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<PlayerDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(PlayerDto playerDto)
        {
            throw new NotImplementedException();
        }
    }
}
