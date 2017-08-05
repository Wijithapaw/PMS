using PMS.Domain.Dtos.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.Services
{
    public interface IPlayerService
    {
        int Create(PlayerDto playerDto);

        PlayerDto Get(int id);

        ICollection<PlayerDto> GetAll();

        void Update(PlayerDto playerDto);

        void Delete(int id);
    }
}
