using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModel;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private readonly Models.kbsContext _dbContext;

        public DashboardController(ILogger<DashboardController> pLogger, Models.kbsContext dbContext)
        {
            _logger = pLogger;
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Booking(int id)
        {
            TblEventDaten eventDaten = await _dbContext.TblEventDatens.FindAsync(id);

            if (eventDaten == null)
            {
                return View();
            }

            BookingViewModel bvm = new();

            bvm.EdEvDatenId = eventDaten.EdEvDatenId;
            bvm.EtEventId = eventDaten.EtEventId;
            bvm.EdPreis = eventDaten.EdPreis;
            bvm.EdBeginn = eventDaten.EdBeginn;
            bvm.EdEnde = eventDaten.EdEnde;
            bvm.EdStartOrt = eventDaten.EdStartOrt;
            bvm.EdZielort = eventDaten.EdZielort;
            bvm.EdMaxTeilnehmer = eventDaten.EdMaxTeilnehmer;
            bvm.EdAktTeilnehmer = eventDaten.EdAktTeilnehmer;
            bvm.EdRabatt = eventDaten.EdRabatt;
            bvm.Available = eventDaten.EdMaxTeilnehmer - eventDaten.EdAktTeilnehmer;
            bvm.EventName = eventDaten.EtEvent.EtBezeichnung;
            bvm.EventDescription = eventDaten.EtEvent.EtBeschreibung;

            return View("Booking", bvm);
        }
    }
}
