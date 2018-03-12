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
        public bool Saved { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }

        /*calculates the IniativeScore for a character.
         * This is done by giving triumps, successes, and advantages a multiplier value. Multiple the value by number of each role to get overall score.
         */
        public void SetIniativeScore(Character character)
        {
            const short TRIUMPH_MULTIPLIER = 250;
            const short SUCCESS_MULTIPLER = 20;
            const short ADVANTAGE_MULTIPLER = 1;
            short score = 0;

            score = (Int16)((character.Triumphs * TRIUMPH_MULTIPLIER) + (character.Succeses * SUCCESS_MULTIPLER) + (character.Advantages + ADVANTAGE_MULTIPLER));
            character.IniativeScore = score;
        }

        public void AddToEncounter(Encounter encounter, Character character)
        {
            //if this list is empty, add character
            if (!encounter.CharactersInEncounter.Any())
            {
                encounter.CharactersInEncounter.Add(character);
            }
            else //if not empty, determine where to add character based on iniativescore
            {
                foreach (Character characterInEncounter in encounter.CharactersInEncounter)
                {
                    if (character.IniativeScore > characterInEncounter.IniativeScore)
                    {

                    }
                }
            }
        }
    }
}
