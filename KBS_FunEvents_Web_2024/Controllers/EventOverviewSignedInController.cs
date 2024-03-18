using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using KBS_FunEvents_Web_2024.Models;
using KBS_FunEvents_Web_2024.ViewModels;

namespace KBS_FunEvents_Web_2024.Controllers
{
    public class EventOverviewSignedInController : Controller
    {
        private readonly _dbContext _context;

        public EventOverviewSignedInController(_dbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = GetEventDataForViewModel();
            return View("Index", vm);        }

        public IActionResult GetAllDatesOfEvent(int eventId)
        {
            var vm = GetDatesForEvents(eventId);

            return View("AllDates", vm);
        }

        private List<EventOverviewViewModel> GetEventDataForViewModel()
        {
            var data = _context.TblEvents.Include(x => x.EkEvKategorie).Include(x => x.EvEvVeranstalter);

            List<EventOverviewViewModel> dataForVM = new List<EventOverviewViewModel>();

            foreach (var eventData in data)
            {

                EventOverviewViewModel ev = new EventOverviewViewModel
                {
                    EtEventId = eventData.EtEventId,
                    EtBezeichnung = eventData.EtBezeichnung,
                    EkKatBezeichnung = eventData.EkEvKategorie.EkKatBezeichnung,
                    EvFirma = eventData.EvEvVeranstalter.EvFirma
                };

                dataForVM.Add(ev);
            }

            return dataForVM;
        }

        private List<EventOverviewViewModel> GetDatesForEvents(int id)
        {
            var eventName = _context.TblEvents.FirstOrDefault(x => x.EtEventId == id).EtBezeichnung;
            var eventDetails = _context.TblEventDatens.Where(x => x.EtEventId == id);

            List<EventOverviewViewModel> dataForVM = new List<EventOverviewViewModel>();

            foreach (var eventDetail in eventDetails)
            {
                EventOverviewViewModel ev = new EventOverviewViewModel
                {
                    EtEventId = eventDetail.EtEventId,
                    EdEvDatenId = eventDetail.EdEvDatenId,
                    EtBezeichnung = eventName,
                    EdBeginn = eventDetail.EdBeginn,
                    EdStartOrt = eventDetail.EdStartOrt,
                    EdPreis = eventDetail.EdPreis,
                    EdAktTeilnehmer = eventDetail.EdAktTeilnehmer,
                    EdMaxTeilnehmer = eventDetail.EdMaxTeilnehmer,                   
                };

                dataForVM.Add(ev);
            }
            return dataForVM;
        }
    }
}
