using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KBS_FunEvents_Web_2024.ViewModels
{
    public class EventOverviewViewModel
    {
        [Key]
        public int EtEventId { get; set; }
        [Display(Name = "Eventbezeichnung")]
        public string EtBezeichnung { get; set; }

        [Display(Name = "Eventkategorie")]
        public string EkKatBezeichnung { get; set; }
        public int EkEvKategorieId { get; set; }

        [Display(Name = "Veranstalter")]
        public string EvFirma { get; set; }
        public int EvEvVeranstalterId { get; set; }



        [Display(Name = "Startort des Events")]
        public string EdStartOrt { get; set; }


        [Display(Name = "Preis pro Teilnehmer")]
        public decimal EdPreis { get; set; }

        public int EdMaxTeilnehmer { get; set; }

        [Display(Name = "Freie Plätze")]
        public int EdAktTeilnehmer { get; set; }


        [Display(Name = "Beginn des Events")]
        public DateTime EdBeginn { get; set; }
    }
}
