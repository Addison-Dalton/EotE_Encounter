using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EotE_Encounter.Controllers
{
    public class CharacterController : Controller
    {
        public ActionResult CreateCharacter()
        {
            return PartialView("Add");
        }
    }
}