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

        /*sets encounter name is "Encounter" if none is given
         *saves encounter to DB
         *
         */
        [HttpPost]
        public ActionResult Create(Encounter encounter)
        {
            //consider doing this in encounter controller
            if (String.IsNullOrWhiteSpace(encounter.Name))
            {
                encounter.Name = "Encounter";
            }

            _context.Encounters.Add(encounter);
            _context.SaveChanges();
            return RedirectToAction("Details", new {encounterId = encounter.Id });
        }

        /* gets the encounter from db based on encounterId.
         * gets list of characters that encounterId's match the encounter.
         * sorts list of characters based on IniativeScore
         */
        public ActionResult Details(int encounterId)
        {
            Encounter encounter = _context.Encounters.Where(e => e.Id == encounterId).SingleOrDefault();
            List<Character> characters = _context.Characters.Where(c => c.Encounter.Id == encounterId).ToList();
            encounter.Characters = encounter.Characters.OrderByDescending(c => c.IniativeScore).ToList();
            return PartialView("Details", encounter);
        }
    }
}