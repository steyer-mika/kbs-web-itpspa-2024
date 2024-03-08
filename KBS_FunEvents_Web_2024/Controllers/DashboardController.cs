using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private kbsContext kbsContext;

        public DashboardController(ILogger<DashboardController> pLogger, kbsContext dbContext)
        {
            _logger = pLogger;
            kbsContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetActiveBookings()
        {
            //int? kundenId = HttpContext.Session.GetInt32("KundenId");
            var result = kbsContext.TblBuchungens.Where(x => x.KdKundenId == 1).Include(x => x.EdEvDaten).Include(x => x.EdEvDaten.EtEvent).ToList();
            return View("Bookings", result);
        }

        [HttpGet]
        public IActionResult GetDetailBookings(int pId)
        {
           // int? kundenId = HttpContext.Session.GetInt32("KundenId");
            var result = kbsContext.TblBuchungens.Where(x => x.KdKundenId == 1 && x.BuBuchungsId == pId).Include(x => x.EdEvDaten).Include(y => y.EdEvDaten.EtEvent).Include(z => z.EdEvDaten.EtEvent.EvEvVeranstalter).Include(v => v.EdEvDaten.EtEvent.EkEvKategorie).ToList();
            return View("BookingDetail", result);
        }

        [HttpGet]
        public IActionResult Stonierung(int pId)
        {
            var booking = kbsContext.TblBuchungens.FirstOrDefault(x => x.BuBuchungsId == pId);
            booking.BuStorniert = true;
            //ToDo: Plätze Freigeben


            kbsContext.Update(booking);
            kbsContext.SaveChanges();
            return RedirectToAction("GetActiveBookings");
        }
    }
}
