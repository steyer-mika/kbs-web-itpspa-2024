﻿using KBS_FunEvents_Web_2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly kbsContext _kbsContext;

        public HomeController(ILogger<HomeController> logger, kbsContext kbsContext)
        {
            _logger = logger;
            _kbsContext = kbsContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Informations()
        {
            return View();
        }

        public IActionResult BookingDetail(int id)
        {

            return RedirectToAction("GetDetailBookings", "Dashboard", new { id = id });

            return View(id);
        }

        public IActionResult Bookings()
        {
            return View();
        }

        public IActionResult GetEvents()
        {
            var result = _kbsContext.TblEvents.Include(x => x.EkEvKategorie).Include(y => y.EvEvVeranstalter).ToList();
            return View("Events", result);
        }

        public IActionResult EventDetails(int evId )
        {
            var result = _kbsContext.TblEventDatens.Where(x => x.EtEventId == evId).Include(x => x.EtEvent).ToList();
            return View("EventDetail", result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
