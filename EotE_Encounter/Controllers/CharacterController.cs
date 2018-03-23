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
                character.EncounterId = encounterId;
                character.Encounter = _context.Encounters.Where(e => e.Id.Equals(encounterId)).SingleOrDefault();
                character.SetIniativeScore();
                _context.Characters.Add(character);
                _context.SaveChanges();
                return RedirectToAction("Details", "Encounter", new {encounterId});
            }
            return PartialView();
        }


        public ActionResult Edit(Character character)
        {
            if (ModelState.IsValid)
            {
                Character oldCharacter = _context.Characters.Where(c => c.Id.Equals(character.Id)).SingleOrDefault();
                
                //if the number if triupmhs, successes, or advantages have changed, update the IniativeScore
                if(oldCharacter.Triumphs != character.Triumphs || oldCharacter.Succeses != character.Succeses || oldCharacter.Advantages != character.Advantages)
                {
                    _context.Entry(oldCharacter).CurrentValues.SetValues(character);
                    oldCharacter.SetIniativeScore();
                }
                else
                {
                    _context.Entry(oldCharacter).CurrentValues.SetValues(character);
                }
                _context.SaveChanges();

                return RedirectToAction("Details", "Encounter", new {encounterId = character.EncounterId });
            }
            return PartialView("Details", character);
        }

        public ActionResult Details(int characterId)
        {
            Character character = _context.Characters.Where(c => c.Id.Equals(characterId)).SingleOrDefault();
            _context.Entry(character).Reload();
            return PartialView("Details", character);
        }

        public ActionResult Delete(int characterId)
        {
            Character character = _context.Characters.Where(c => c.Id == characterId).SingleOrDefault();
            _context.Characters.Remove(character);
            _context.SaveChanges();
            return RedirectToAction("Details", "Encounter", new { encounterId = character.EncounterId });
        }
    }
}