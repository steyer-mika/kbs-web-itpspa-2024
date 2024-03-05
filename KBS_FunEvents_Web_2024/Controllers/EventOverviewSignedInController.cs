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

        // GET: EventViewModels
        public async Task<IActionResult> Index()
        {

            var vm = GetData();

            return View("Index", vm);
        }

        // GET: EventViewModels/Details/5

        private List<EventOverviewViewModel> GetData()
        {
            var data = _context.TblEvents;

            List<EventOverviewViewModel> dataForVM = new List<EventOverviewViewModel>();

            foreach (var eventData in data)
            {
                EventOverviewViewModel ev = new EventOverviewViewModel
                {
                    EtEventId = eventData.EtEventId,
                    EtBezeichnung = eventData.EtBezeichnung,
                };

                dataForVM.Add(ev);
            }

            return dataForVM;

        }
    }
}
