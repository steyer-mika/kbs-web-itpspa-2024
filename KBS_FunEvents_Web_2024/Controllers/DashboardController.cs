using KBS_FunEvents_Web_2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using Microsoft.EntityFrameworkCore;
using KBS_FunEvents_Web_2024.ViewModels;
using Microsoft.AspNetCore.Http;
using KBS_FunEvents_Web_2024.ComputeHash;

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
            int? id = HttpContext.Session.GetInt32("KundenID");

            if (id == null) return BadRequest();
            DashboardModelView mv = new DashboardModelView();
            TblKunden kundenDaten = _dbContext.TblKundens.Where(k => k.KdKundenId == id).FirstOrDefault();
            TblBuchungen buchungsDaten = _dbContext.TblBuchungens.Include(x => x.EdEvDaten).ThenInclude(x => x.EtEvent).Where(b => b.KdKundenId == id && b.BuStorniert == false && b.EdEvDaten.EdBeginn > System.DateTime.Today).OrderBy(b => b.EdEvDaten.EdBeginn).FirstOrDefault();
            TblEventDaten eventDaten = _dbContext.TblEventDatens.Find(id);

            if (eventDaten != null)
            {
                TblEvent baseEvent = _dbContext.TblEvents.Find(eventDaten.EtEventId);
                mv.EdBeginn = buchungsDaten.EdEvDaten.EdBeginn;
                mv.EtBeschreibung = buchungsDaten.EdEvDaten.EtEvent.EtBeschreibung;
                mv.EtBezeichnung = buchungsDaten.EdEvDaten.EtEvent.EtBezeichnung;
            }
            
            mv.NumDurchgefuehrteEvents = _dbContext.TblBuchungens.Where(b => b.KdKundenId == id && b.BuStorniert == false && b.EdEvDaten.EdEnde < System.DateTime.Today).Count();
            mv.NumAktiveBuchungen = _dbContext.TblBuchungens.Where(b => b.KdKundenId == id && b.BuStorniert == false && b.EdEvDaten.EdBeginn > System.DateTime.Today).Count();
            mv.NumStornierteBuchungen = _dbContext.TblBuchungens.Where(b => b.KdKundenId == id && b.BuStorniert == true).Count();

            return View(mv);
        }

        public async Task<IActionResult> Booking(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }

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

            return (ActionResult)GetDetailBookings(booking.BuBuchungsId);
        }

        [HttpGet]
        public IActionResult GetActiveBookings()
        {
            int? kundenId = HttpContext.Session.GetInt32("KundenID");
            var result = _dbContext.TblBuchungens.Where(x => x.KdKundenId == kundenId && x.BuStorniert == false).Include(x => x.EdEvDaten).Include(x => x.EdEvDaten.EtEvent).ToList();
            return View("Bookings", result);
        }

        [HttpGet]
        public IActionResult GetDetailBookings(int pId)
        {
            int? kundenId = HttpContext.Session.GetInt32("KundenID");
            var result = _dbContext.TblBuchungens.Where(x => x.KdKundenId == kundenId && x.BuBuchungsId == pId).Include(x => x.EdEvDaten).Include(y => y.EdEvDaten.EtEvent).Include(z => z.EdEvDaten.EtEvent.EvEvVeranstalter).Include(v => v.EdEvDaten.EtEvent.EkEvKategorie).ToList();
            return View("BookingDetail", result);
        }

        [HttpGet]
        public IActionResult Stonierung(int pId)
        {
            var booking = _dbContext.TblBuchungens.Include(x => x.EdEvDaten).FirstOrDefault(x => x.BuBuchungsId == pId);
            int stoniertePlaetze = booking.BuGebuchtePlaetze;
            booking.BuStorniert = true;
            booking.BuGebuchtePlaetze = booking.BuGebuchtePlaetze - stoniertePlaetze;
            booking.EdEvDaten.EdAktTeilnehmer = booking.EdEvDaten.EdAktTeilnehmer - stoniertePlaetze;

            _dbContext.Update(booking);
            _dbContext.SaveChanges();
            return RedirectToAction("GetActiveBookings");
        }
    }
}
