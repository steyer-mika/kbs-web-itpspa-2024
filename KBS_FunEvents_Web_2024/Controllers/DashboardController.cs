using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModel;
using Microsoft.AspNetCore.Http;

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

            TblEvent baseEvent = await _dbContext.TblEvents.FindAsync(eventDaten.EtEventId);

            bvm.EventName = baseEvent.EtBezeichnung;
            bvm.EventDescription = baseEvent.EtBeschreibung;

            return View("Booking", bvm);
        }

        [HttpPost]
        public async Task<IActionResult> Booking(int eventDataId, int bookedPlaces)
        {
            if (ModelState.IsValid == false) return View();

            TblEventDaten eventDaten = _dbContext.TblEventDatens.Where(t => t.EdEvDatenId == eventDataId).FirstOrDefault();

            if (eventDaten == null) return View();

            if (eventDaten.EdAktTeilnehmer + bookedPlaces > eventDaten.EdMaxTeilnehmer)
            {
                return View();
            }

            int? customerId = HttpContext.Session.GetInt32("KundenID");
            if (customerId == null) return View();

            TblBuchungen booking = new();
            booking.BuBezahlt = false;
            booking.BuGebuchtePlaetze = bookedPlaces;
            booking.BuStorniert = false;
            booking.BuRechnungErstellt = false;
            booking.EdEvDatenId = eventDataId;
            booking.KdKundenId = customerId.Value;
            _dbContext.TblBuchungens.Add(booking);

            eventDaten.EdAktTeilnehmer += bookedPlaces;
            _dbContext.TblEventDatens.Update(eventDaten);

            await _dbContext.SaveChangesAsync();

            return View();
        }
    }
}
