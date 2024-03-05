using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private kbsContext kbsContext;
        private int _kundenId;

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
            var result = kbsContext.TblBuchungens.Where(x => x.KdKundenId == _kundenId);
            return View(result);
        }

        [HttpGet]
        public IActionResult GetDetailBookings(int pId)
        {
            var result = kbsContext.TblBuchungens.Where(x => x.KdKundenId == _kundenId || x.BuBuchungsId == pId);
            return View(result);
        }

        [HttpPost]
        public IActionResult Stonierung(int pId)
        {
            var booking = kbsContext.TblBuchungens.FirstOrDefault(x => x.BuBuchungsId == pId);
            booking.BuStorniert = true;
            return View();
        }
    }
}
