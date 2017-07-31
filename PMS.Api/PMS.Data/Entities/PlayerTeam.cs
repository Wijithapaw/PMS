using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PMS.Data.Entities
{
    public class PlayerTeam
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [Required]
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual Player Player { get; set; }

        public virtual Team Team { get; set; }
    }
}
