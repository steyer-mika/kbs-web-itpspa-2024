using KBS_FunEvents_Web_2024.ComputeHash;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModels;
using Microsoft.AspNetCore.Http;
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

        private readonly kbsContext _dbContext;

        public HomeController(ILogger<HomeController> logger, kbsContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.kundenId = HttpContext.Session.GetInt32("KundenID");
            ViewBag.email = HttpContext.Session.GetString("Email");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Login(LoginModelView login)
        {
            if (ModelState.IsValid)
            {
                var email = login.KdEmail;
                var password = MD5Generator.getMD5Hash(login.KdPwHash);

                TblKunden customer = await _dbContext.TblKundens.FirstOrDefaultAsync(x => x.KdEmail == email && x.KdPasswortHash == password);

                if(customer != null)
                {
                    HttpContext.Session.SetInt32("KundenID", customer.KdKundenId);
                    HttpContext.Session.SetString("Email", customer.KdEmail);

                    return RedirectToAction(controllerName: "Home", actionName: "Privacy");
                }
            }

            return View(login);
        }
        public IActionResult Registration()
        {
            return View();
        }
    }
}
