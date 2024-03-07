using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using Microsoft.EntityFrameworkCore;
using KBS_FunEvents_Web_2024.ViewModels;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private readonly kbsContext _kbsContext;

        public DashboardController(ILogger<DashboardController> pLogger, kbsContext kbsContext)
        {
            _logger = pLogger;
            _kbsContext = kbsContext;
        }

        public IActionResult Index()
        {
            int id = 1; 
            TblEventDaten eventDaten = _kbsContext.TblEventDatens.Find(id);
            
            if (eventDaten == null)
            {
                return View();
            }

            TblEvent baseEvent = _kbsContext.TblEvents.Find(eventDaten.EtEventId);
            
            DashboardModelView mv = new DashboardModelView();

            mv.EdBeginn = eventDaten.EdBeginn;
            mv.EtBeschreibung = baseEvent.EtBeschreibung;
            mv.EtBezeichnung = baseEvent.EtBezeichnung;

            return View(mv);
        }

     
    }
}
