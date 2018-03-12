using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EotE_Encounter.Models
{
    public class Character
    {
        public int Id { get; set; }
        [Required][StringLength(250)]
        public string Name { get; set; }
        public byte Triumphs { get; set; }
        public byte Succeses { get; set; }
        public byte Advantages { get; set; }
        public short IniativeScore { get; set; } //this is calculated based on # of Triumphs, successes, and advantages
        public byte InitativeIndex { get; set; }
        public bool Saved { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }

    }
}
