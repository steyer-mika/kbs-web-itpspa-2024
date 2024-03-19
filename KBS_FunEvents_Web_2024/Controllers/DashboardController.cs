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
using KBS_FunEvents_Web_2024.ComputeHash;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class DashboardController : Controller
    {
        private ILogger<DashboardController> _logger;
        private readonly kbsContext _dbContext;

        public DashboardController(ILogger<DashboardController> pLogger, kbsContext kbsContext)
        {
            _logger = pLogger;
            _dbContext = kbsContext;
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

        public IActionResult ChangePassword(ChangePasswordModelView changing)
        {
            int? id = HttpContext.Session.GetInt32("KundenID");
            var currentPassword = MD5Generator.getMD5Hash(changing.CurrentPasswort);
            var password = changing.Passwort;
            var passwordWdh = changing.PasswortWDH;

            if(id != null)
            {
                TblKunden existingCustomer = _dbContext.TblKundens.Where(k => k.KdKundenId == id).FirstOrDefault();

                if (currentPassword.Equals(existingCustomer.KdPasswortHash) && password.Equals(passwordWdh))
                {
                    existingCustomer.KdPasswortHash = MD5Generator.getMD5Hash(password);
                    _dbContext.TblKundens.Update(existingCustomer);
                    _dbContext.SaveChanges();
                }
            }

            return View();
        }

    }
}
