using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.Dtos.Player
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string HomeCountry { get; set; }

        public DateTime Birthday { get; set; }
    }
}
