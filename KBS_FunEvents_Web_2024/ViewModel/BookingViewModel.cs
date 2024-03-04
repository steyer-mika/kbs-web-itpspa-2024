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

        [Display(Name = "Beginn")]
        public DateTime EdBeginn { get; set; }

        [Display(Name = "Ende")]
        public DateTime EdEnde { get; set; }

        [Display(Name = "Startort")]
        public string EdStartOrt { get; set; }

        [Display(Name = "Zielort")]
        public string EdZielort { get; set; }

        public int EdMaxTeilnehmer { get; set; }

        public int EdAktTeilnehmer { get; set; }

        public bool EdFreigegeben { get; set; }

        public decimal EdRabatt { get; set; }

        public bool EdVeranstalterBenachrichtigt { get; set; }
    }
}
