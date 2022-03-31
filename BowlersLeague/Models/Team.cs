using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BowlersLeague.Models
{
    public class Team
    {
        [Key]
        [Required]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int CaptainID { get; set; }
    }
}
