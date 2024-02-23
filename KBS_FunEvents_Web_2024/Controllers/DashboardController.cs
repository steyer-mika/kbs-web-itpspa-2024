using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> pLogger)
        {
            _logger = pLogger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
