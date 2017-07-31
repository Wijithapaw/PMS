using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.Dtos.Team
{
    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}
