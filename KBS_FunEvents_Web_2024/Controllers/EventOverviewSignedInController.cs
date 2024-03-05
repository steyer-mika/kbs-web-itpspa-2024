using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModels;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class EventOverviewSignedInController : Controller
    {
        private readonly kbsContext _context;

        public EventOverviewSignedInController(kbsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            EventOverviewViewModel evViewModels = GetAllEventModels();
            return View(evViewModels);
        }

        private EventOverviewViewModel GetAllEventModels()
        {
            var allEvents = _context.TblEvents;
            EventOverviewViewModel evViewModels = new EventOverviewViewModel();

            foreach (var item in allEvents)
            {
                EventViewModel newViewModel = new EventViewModel();
                
                newViewModel.EtEventId = item.EtEventId;
                newViewModel.EtBezeichnung = item.EtBezeichnung;
                newViewModel.EkEvKategorieId = item.EkEvKategorieId;
                newViewModel.EvEvVeranstalterId = item.EvEvVeranstalterId;

                evViewModels.eventViewModels.Add(newViewModel);
            }

            return evViewModels;
        }
    }
}
