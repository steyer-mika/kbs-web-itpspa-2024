﻿using KBS_FunEvents_Web_2024.ComputeHash;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly _dbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, _dbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [RequireHttps]
        public IActionResult Index()
        {
            return View();
        }

        [RequireHttps]
        public IActionResult Privacy()
        {
            ViewBag.kundenId = HttpContext.Session.GetInt32("KundenID");
            ViewBag.email = HttpContext.Session.GetString("Email");
            return View();
        }


        [RequireHttps]
        public IActionResult Contact()
        {
            return View();
        }

        [RequireHttps]
        public IActionResult Informations()
        {
            return View();
        }

        public IActionResult GetEvents()
        {
            if (HttpContext.Session.GetInt32("KundenID") != null)
                return RedirectToAction(controllerName: "EventOverviewSignedIn", actionName: "Index");

            var result = _dbContext.TblEvents.Include(x => x.EkEvKategorie).Include(y => y.EvEvVeranstalter).Include(z => z.TblEventDatens.Where(x => x.EdFreigegeben == true)).ToList();
            return View("Events", result);
        }

        public IActionResult EventDetails(int evId)
        {
            var result = _dbContext.TblEventDatens.Where(x => x.EtEventId == evId).Include(x => x.EtEvent).ToList();
            return View("EventDetail", result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            LoginModelView login = new();
            return View(login);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [RequireHttps, HttpPost]
        public async Task<IActionResult> Login(LoginModelView login)
        {
            if (ModelState.IsValid)
            {
                var email = login.KdEmail;
                var password = MD5Generator.getMD5Hash(login.KdPwHash);

                TblKunden customer = await _dbContext.TblKundens.FirstOrDefaultAsync(x => x.KdEmail.ToLower() == email.ToLower());

                if (customer != null)
                {
                    if (customer.KdPasswortHash == password)
                    {
                        HttpContext.Session.SetInt32("KundenID", customer.KdKundenId);
                        HttpContext.Session.SetString("Email", customer.KdEmail);


                        return RedirectToAction(controllerName: "Dashboard", actionName: "Index");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(login.KdPwHash), "Inkorrektes Passwort!");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(login.KdEmail), "Es ist kein Account für diese E-mail vorhanden, bitte registrieren Sie sich!");
                }
            }
            
            return View(login);
        }

        [RequireHttps, HttpPost]
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
                TblKunden existingCustomer = await _dbContext.TblKundens
                    .FirstOrDefaultAsync(x => mail.ToLower() == x.KdEmail.ToLower() || nname.ToLower() == x.KdName.ToLower() && vname.ToLower() == x.KdVorname.ToLower()
                    && str.ToLower() == x.KdStrasse.ToLower() && hnummer.ToLower() == x.KdHnummer.ToLower() && plz.ToLower() == x.KdPlz.ToLower() && ort.ToLower() == x.KdOrt.ToLower());
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
                        existingCustomer.KdTelefon = tel;
                        existingCustomer.KdEmail = mail;
                        existingCustomer.KdPasswortHash = MD5Generator.getMD5Hash(password);
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

        [RequireHttps]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

    }
}
