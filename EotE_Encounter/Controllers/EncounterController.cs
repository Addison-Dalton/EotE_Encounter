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

        /*changes the iniative order of the character passed. This is done by swapping the iniativeScore of the 
         * character passed with either the character below or above it, which is passed via the direction variable.
         * It the character is at the top of the list, and moved up then the character is moved to the bottom of the list.
         * The opposite occurs if the character is at the bottom of the list.
         */
        public ActionResult ChangeInitiative(int characterId, string direction)
        {
            const string MOVE_UP = "up";
            const string MOVE_DOWN = "down";
            List<Character> characters = _context.Characters.OrderByDescending(c => c.IniativeScore).ToList();
            Character movedCharacter = _context.Characters.Where(c => c.Id == characterId).SingleOrDefault();

            //move character up
            if(direction == MOVE_UP)
            {
                //detect if character is at the top of the list
                if(movedCharacter.Id != characters.FirstOrDefault().Id)
                {
                    short tempInitiativeScore = movedCharacter.IniativeScore;
                    int movedCharacterIndex = characters.IndexOf(movedCharacter);
                    movedCharacter.IniativeScore = characters[movedCharacterIndex - 1].IniativeScore;
                    characters[movedCharacterIndex - 1].IniativeScore = tempInitiativeScore;

                    if(characters[movedCharacterIndex - 1].Turn == true)
                    {
                        characters[movedCharacterIndex - 1].Turn = false;
                        movedCharacter.Turn = true;
                    }
                }
                else
                {
                    
                    for(int i = 1; i < characters.Count; i++)
                    {
                        short tempInitiativeScore = movedCharacter.IniativeScore;
                        movedCharacter.IniativeScore = characters[i].IniativeScore;
                        characters[i].IniativeScore = tempInitiativeScore;
                    }

                    if (movedCharacter.Turn == true)
                    {
                        int movedCharacterIndex = characters.IndexOf(movedCharacter);
                        movedCharacter.Turn = false;
                        characters[movedCharacterIndex + 1].Turn = true;
                    }
                }
            }else if (direction == MOVE_DOWN)
            {
                if(movedCharacter.Id != characters.LastOrDefault().Id)
                {
                    short tempInitiativeScore = movedCharacter.IniativeScore;
                    int movedCharacterIndex = characters.IndexOf(movedCharacter);
                    movedCharacter.IniativeScore = characters[movedCharacterIndex + 1].IniativeScore;
                    characters[movedCharacterIndex + 1].IniativeScore = tempInitiativeScore;

                    if (movedCharacter.Turn == true)
                    {
                        movedCharacter.Turn = false;
                        characters[movedCharacterIndex + 1].Turn = true;
                    }
                }
                else
                {
                    for(int i = characters.Count - 2; i >= 0; i--)
                    {
                        short tempInitiativeScore = movedCharacter.IniativeScore;
                        movedCharacter.IniativeScore = characters[i].IniativeScore;
                        characters[i].IniativeScore = tempInitiativeScore;
                    }
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Details", new { encounterId = movedCharacter.EncounterId });
        }

        //deletes all characters from encounter
        //Saved character's have their EncounterId set to null
        public ActionResult Clear()
        {
            List<Character> characters = _context.Characters.ToList();
            int? tempEncounterId = characters[0].EncounterId;
            foreach(Character character in characters)
            {
                if(character.Saved != true)
                {
                    _context.Characters.Remove(character);
                }
                else
                {
                    character.EncounterId = null;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Details", new { encounterId = tempEncounterId });

        }
    }
}