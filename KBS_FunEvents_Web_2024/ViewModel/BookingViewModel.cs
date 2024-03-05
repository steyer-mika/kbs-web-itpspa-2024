using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KBS_FunEvents_Web_2024.Models;

namespace KBS_FunEvents_Web_2024.ViewModel
{
    public class BookingViewModel
    {
        [Key]
        public int EdEvDatenId { get; set; }

        public int EtEventId { get; set; }

        [Display(Name = "Preis")]
        public decimal EdPreis { get; set; }

        [Display(Name = "Start des Events")]
        public DateTime EdBeginn { get; set; }

        [Display(Name = "Ende des Events")]
        public DateTime EdEnde { get; set; }

        [Display(Name = "Startort")]
        public string EdStartOrt { get; set; }

        [Display(Name = "Zielort")]
        public string EdZielort { get; set; }

        [Display(Name = "Freie Plätze")]
        public int Available { get; set; }

        public int EdMaxTeilnehmer { get; set; }

        public int EdAktTeilnehmer { get; set; }

        public decimal EdRabatt { get; set; }

        [Display(Name = "Name des Events")]
        public string EventName { get; set; }

        [Display(Name = "Beschreibung des Events")]
        public string EventDescription { get; set; }
    }
}
