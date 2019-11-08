using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.BarMagician.Models;

namespace Cocktail_Magician.Areas.BarCrower.Controllers
{
    [Area("BarCrower")]
    public class BarsController : Controller
    {
        private readonly IBarManager _barManager;

        public BarsController(IBarManager barManager)
        {
            _barManager = barManager;
        }


        // GET: BarCrower/Bars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = await _barManager.GetBar(id);
            if (bar == null)
            {
                return NotFound();
            }
            var barViewModel = new BarViewModel(bar);
            return View(barViewModel);
        }
    }
}
