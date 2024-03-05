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

                if (customer != null)
                {
                    HttpContext.Session.SetInt32("KundenID", customer.KdKundenId);
                    HttpContext.Session.SetString("Email", customer.KdEmail);

                    return RedirectToAction(controllerName: "Home", actionName: "Privacy");
                }
            }

            return View(login);
        }
        public async Task<IActionResult> Registration(RegistrationModelView registration)
        {
            if (ModelState.IsValid)
            {
                var nname = registration.Nachname;
                var vname = registration.Vorname;
                var str = registration.Strasse;
                var hnummer = registration.Hausnummer;
                var plz = registration.Postleitzahl;
                var ort = registration.Ort;
                var mail = registration.KdEmail;
                var tel = registration.Telefon;
                var password = registration.Passwort;
                TblKunden existingCustomer = await _dbContext.TblKundens.FirstOrDefaultAsync(x => mail == x.KdEmail || nname == x.KdName && vname == x.KdVorname && str == x.KdStrasse && hnummer == x.KdHnummer && plz == x.KdPlz && ort == x.KdOrt);
                if (existingCustomer == null)
                {
                    await _dbContext.AddAsync(new TblKunden
                    {
                        KdName = nname,
                        KdVorname = vname,
                        KdStrasse = str,
                        KdHnummer = hnummer,
                        KdPlz = plz,
                        KdOrt = ort,
                        KdEmail = mail,
                        KdTelefon = tel,
                        KdPasswortHash = MD5Generator.getMD5Hash(password)
                    }); ;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    if (string.IsNullOrEmpty(existingCustomer.KdEmail))
                    {
                        existingCustomer.KdEmail = mail;
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(registration.KdEmail), "Es existiert bereits ein Nutzer mit dieser Email");
                        return View();
                    }
                }
                return RedirectToAction(controllerName: "Home", actionName: "Index");
            }
            return View();
        }
    }
}
