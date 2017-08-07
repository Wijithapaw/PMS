using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.Dtos.Player
{
    public class PlayerTeamDto
    {
        public int PlayerId { get; set; }

        public int TeamId { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
