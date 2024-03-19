using KBS_FunEvents_Web_2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private readonly Models._dbContext _dbContext;

        public DashboardController(ILogger<DashboardController> pLogger, Models._dbContext dbContext)
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
        public ActionResult Booking([Bind("EdEvDatenId,BookedPlaces")] BookingViewModel bookingViewModel)
        {
            int eventDataId = bookingViewModel.EdEvDatenId;
            int bookedPlaces = bookingViewModel.BookedPlaces;

            TblEventDaten eventDaten = _dbContext.TblEventDatens.Where(t => t.EdEvDatenId == eventDataId).FirstOrDefault();

            if (eventDaten == null) return View("Index");

            if (eventDaten.EdAktTeilnehmer + bookedPlaces > eventDaten.EdMaxTeilnehmer)
            {
                return View("Index");
            }

            int? customerId = HttpContext.Session.GetInt32("KundenID");
            if (customerId == null) return View("Index");

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

            _dbContext.SaveChanges();

            return View("Index");
        }

        [HttpGet]
        public IActionResult GetActiveBookings()
        {
            int? kundenId = HttpContext.Session.GetInt32("KundenId");
            var result = _dbContext.TblBuchungens.Where(x => x.KdKundenId == kundenId).Include(x => x.EdEvDaten).Include(x => x.EdEvDaten.EtEvent).ToList();
            return View("Bookings", result);
        }

        [HttpGet]
        public IActionResult GetDetailBookings(int pId)
        {
            int? kundenId = HttpContext.Session.GetInt32("KundenId");
            var result = _dbContext.TblBuchungens.Where(x => x.KdKundenId == kundenId && x.BuBuchungsId == pId).Include(x => x.EdEvDaten).Include(y => y.EdEvDaten.EtEvent).Include(z => z.EdEvDaten.EtEvent.EvEvVeranstalter).Include(v => v.EdEvDaten.EtEvent.EkEvKategorie).ToList();
            return View("BookingDetail", result);
        }

        [HttpGet]
        public IActionResult Stonierung(int pId)
        {
            var booking = _dbContext.TblBuchungens.FirstOrDefault(x => x.BuBuchungsId == pId);
            booking.BuStorniert = true;
            booking.BuGebuchtePlaetze = booking.BuGebuchtePlaetze - 1;
            
            _dbContext.Update(booking);
            _dbContext.SaveChanges();
            return RedirectToAction("GetActiveBookings");
        }
    }
}
