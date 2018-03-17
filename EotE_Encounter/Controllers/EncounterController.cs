using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EotE_Encounter.Models;
using EotE_Encounter.Data;
using Microsoft.EntityFrameworkCore;

namespace EotE_Encounter.Controllers
{
    public class EncounterController : Controller
    {
        private readonly EncounterContext _context;

        public EncounterController(EncounterContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult Create(Encounter encounter)
        {
            
            //eventaully I will add the Encounter as a table to the DB.
            if (String.IsNullOrWhiteSpace(encounter.Name))
            {
                encounter.Name = "Encounter";
            }
            _context.Encounters.Add(encounter);
            _context.SaveChanges();
            return RedirectToAction("Details", new {encounterId = encounter.Id });
        }

        public ActionResult Details(int encounterId)
        {
            Encounter encounter = _context.Encounters.Where(e => e.Id == encounterId).SingleOrDefault();
            List<Character> characters = _context.Characters.Where(c => c.Encounter.Id == encounterId).ToList();
            encounter.Characters.Sort((x, y) => x.IniativeScore.CompareTo(y.IniativeScore));
            return PartialView("Details", encounter);
        }
    }
}