using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS_FunEvents_Web_2024.ViewModel
{
    public class BookingViewModel
    {
        [Key]
        public int BuBuchungsId { get; set; }

        [Display(Name = "Kundennummer")]
        public int KdKundenId { get; set; }

        public int EdEvDatenId { get; set; }

        [Display(Name = "Gebuchte Plaetze")]
        public int BuGebuchtePlaetze { get; set; }

        [Display(Name = "bezahlt")]
        public bool BuBezahlt { get; set; }

        [Display(Name = "storniert")]
        public bool BuStorniert { get; set; }

        [Display(Name = "Rechnung erstellt am")]
        public bool BuRechnungErstellt { get; set; }
    }
}
