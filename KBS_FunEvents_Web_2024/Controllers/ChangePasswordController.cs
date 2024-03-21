using KBS_FunEvents_Web_2024.ComputeHash;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class ChangePasswordController : Controller
    {
        private ILogger<ChangePasswordController> _logger;
        private readonly _dbContext _dbContext;

        public ChangePasswordController(ILogger<ChangePasswordController> pLogger, _dbContext kbsContext)
        {
            _logger = pLogger;
            _dbContext = kbsContext;
        }

        public IActionResult ChangePassword()
        {
            ChangePasswordModelView cpmv = new ChangePasswordModelView();

            return View(cpmv);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModelView changing)
        {
            if (ModelState.IsValid)
            {
                int? id = HttpContext.Session.GetInt32("KundenID");
                var currentPassword = MD5Generator.getMD5Hash(changing.CurrentPassword);
                var password = changing.Passwort;
                var passwordWdh = changing.PasswortWDH;

                if (id != null)
                {
                    TblKunden existingCustomer = _dbContext.TblKundens.Where(k => k.KdKundenId == id).FirstOrDefault();

                    if (currentPassword.Equals(existingCustomer.KdPasswortHash) && password.Equals(passwordWdh))
                    {
                        existingCustomer.KdPasswortHash = MD5Generator.getMD5Hash(password);
                        _dbContext.TblKundens.Update(existingCustomer);
                        _dbContext.SaveChanges();

                        return RedirectToAction(controllerName: "Home", actionName: "Logout");
                    }
                }
            }
            return View();
        }
        
    }
}
