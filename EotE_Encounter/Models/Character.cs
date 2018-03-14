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
        public Encounter Encounter { get; set; }
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

        public void AddToEncounter(Encounter encounter, Character newCharacter)
        {
            //add character to list, then sort by iniativeScore
            encounter.CharactersInEncounter.Add(newCharacter);
            encounter.CharactersInEncounter.Sort((x, y) => x.IniativeScore.CompareTo(y.IniativeScore));

            //MAY NOT NEED ANY OF THIS IF ABOVE CODE WORKS
            /*
            //if this list is empty, add character
            if (!encounter.CharactersInEncounter.Any())
            {
                encounter.CharactersInEncounter.Add(newCharacter);
            }
            else //if not empty, determine where to add character based on iniativescore
            {
                foreach (Character characterInEncounter in encounter.CharactersInEncounter)
                {
                    int index = encounter.CharactersInEncounter.IndexOf(characterInEncounter);
                    if (newCharacter.IniativeScore > characterInEncounter.IniativeScore)
                    {
                        encounter.CharactersInEncounter.Insert(index, newCharacter);
                        break;
                    }else if (newCharacter.IniativeScore == characterInEncounter.IniativeScore) //if same score, choose one based on 50/50 chance for each
                    {
                        if(DetermineTie(newCharacter, characterInEncounter) == newCharacter.Id)
                        {
                            encounter.CharactersInEncounter.Insert(index, newCharacter);
                            break;
                        }
                        else
                        {
                            if(index == encounter.CharactersInEncounter.Count() - 1) //add to end of list if reached end of list
                            {
                                encounter.CharactersInEncounter.Add(newCharacter);
                            }
                            else
                            {
                                encounter.CharactersInEncounter.Insert(index + 1, newCharacter);
                            }
                        }
                    }
                    else if (index == encounter.CharactersInEncounter.Count() - 1) //add to end of list if reached end of list
                    {
                        encounter.CharactersInEncounter.Add(newCharacter);
                    }
                }
            }*/
        }

        //returns the Id (int) of the character randomly chosen. Each character has 50/50 chance to be chosen.
        private int DetermineTie(Character characterA, Character characterB)
        {
            Random random = new Random();
            if(random.Next(1, 11) >= 5)
            {
                return characterA.Id;
            }
            else
            {
                return characterB.Id;
            }
        }
    }
}
