using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KBS_FunEvents_Web_2024.Models;
using Microsoft.EntityFrameworkCore;
using KBS_FunEvents_Web_2024.ViewModels;
using Microsoft.AspNetCore.Http;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private readonly kbsContext _kbsContext;

        public DashboardController(ILogger<DashboardController> pLogger, kbsContext kbsContext)
        {
            _logger = pLogger;
            _kbsContext = kbsContext;
        }

        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("KundenID");

            if (id == null) return BadRequest();
            DashboardModelView mv = new DashboardModelView();
            TblKunden kundenDaten = _kbsContext.TblKundens.Where(k => k.KdKundenId == id).FirstOrDefault();
            TblBuchungen buchungsDaten = _kbsContext.TblBuchungens.Include(x => x.EdEvDaten).ThenInclude(x => x.EtEvent).Where(b => b.KdKundenId == id && b.BuStorniert == false && b.EdEvDaten.EdBeginn > System.DateTime.Today).OrderBy(b => b.EdEvDaten.EdBeginn).FirstOrDefault();
            TblEventDaten eventDaten = _kbsContext.TblEventDatens.Find(id);

            if (eventDaten != null)
            {
                TblEvent baseEvent = _kbsContext.TblEvents.Find(eventDaten.EtEventId);
                mv.EdBeginn = buchungsDaten.EdEvDaten.EdBeginn;
                mv.EtBeschreibung = buchungsDaten.EdEvDaten.EtEvent.EtBeschreibung;
                mv.EtBezeichnung = buchungsDaten.EdEvDaten.EtEvent.EtBezeichnung;
            }
            
            mv.NumDurchgefuehrteEvents = _kbsContext.TblBuchungens.Where(b => b.KdKundenId == id && b.BuStorniert == false && b.EdEvDaten.EdEnde < System.DateTime.Today).Count();
            mv.NumAktiveBuchungen = _kbsContext.TblBuchungens.Where(b => b.KdKundenId == id && b.BuStorniert == false && b.EdEvDaten.EdBeginn > System.DateTime.Today).Count();
            mv.NumStornierteBuchungen = _kbsContext.TblBuchungens.Where(b => b.KdKundenId == id && b.BuStorniert == true).Count();

            return View(mv);
        }

        public IActionResult ChangePassword(ChangePasswordModelView changing)
        {
            int? id = HttpContext.Session.GetInt32("KundenID");
            var password = changing.Passwort;
            var passwordWdh = changing.PasswortWDH;

            if (password.Equals(passwordWdh))
            {
                // TODO:
                // MD5Generator.getMD5Hash(password)
                
            }

            return View();
        }

    }

    /*
    public Task<IActionResult> ChangePassword(ChangePasswordModelView changing)
    {
        int? id = HttpContext.Session.GetInt32("KundenID");
        var password = changing.Passwort;
        var passwordWdh = changing.PasswortWDH;

        if (password.Equals(passwordWdh))
        {
                // TODO:
                // MD5Generator.getMD5Hash(password)
        }
        /*
        else
        {
            if (string.IsNullOrEmpty(existingCustomer.KdEmail))
            {
                existingCustomer.KdName = nname;
                existingCustomer.KdVorname = vname;
                existingCustomer.KdStrasse = str;
                existingCustomer.KdHnummer = hnummer;
                existingCustomer.KdPlz = plz;
                existingCustomer.KdOrt = ort;
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
        return Task.FromResult(View());
    }*/

}
