using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EotE_Encounter.Models;
using EotE_Encounter.Data;

namespace EotE_Encounter.Controllers
{
    public class CharacterController : Controller
    {
        private readonly EncounterContext _context;

        public CharacterController(EncounterContext context)
        {
            _context = context;
        }

        public ActionResult CreateCharacter(int encounterId)
        {
            ViewBag.EncounterId = encounterId;
            return PartialView("Add");
        }
        
        
       public ActionResult Add(Character character, int encounterId)
        {
            if (ModelState.IsValid)
            {
                character.Encounter = _context.Encounters.Where(e => e.Id.Equals(encounterId)).SingleOrDefault();
                character.SetIniativeScore(character);
                _context.Characters.Add(character);
                _context.SaveChanges();
                return RedirectToAction("Details", "Encounter", new {encounterId});
            }
            return PartialView();
        }
    }
}