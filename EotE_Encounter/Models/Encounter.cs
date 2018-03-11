﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EotE_Encounter.Models;

namespace EotE_Encounter.Models
{
    public class Encounter
    {
        //these first three properties may not be used just yet.
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Round { get; set; }
        public List<Character> CharactersInEncounter { get; set; }
        [StringLength(2000)]
        public string Notes { get; set; }
    }
}
