using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModel;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class EventOverviewSignedInController : Controller
    {
        private readonly kbsContext _context;

        public EventOverviewSignedInController(kbsContext context)
        {
            _context = context;
        }

        // GET: EventViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventViewModel.ToListAsync());
        }

        // GET: EventViewModels/Details/5


        private bool EventViewModelExists(int id)
        {
            return _context.EventViewModel.Any(e => e.EtEventId == id);
        }
    }
}
