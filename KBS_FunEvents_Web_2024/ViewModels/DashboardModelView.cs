using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KBS_FunEvents_Web_2024.ViewModels
{
    public class DashboardModelView
    {
        [Key]
        public int id { get; set; }

        [Column("ed_Beginn", TypeName = "datetime")]
        public DateTime? EdBeginn { get; set; }

        [Column("AnzDurchgeführteEvents", TypeName = "int")]
        public int NumDurchgefuehrteEvents { get; set; }
        [Column("AnzAktiveBuchungen", TypeName = "int")]
        public int NumAktiveBuchungen { get; set; }
        [Column("AnzStornierteBuchungen", TypeName = "int")]
        public int NumStornierteBuchungen { get; set; }


        [Required]
        [Column("et_Bezeichnung")]
        [StringLength(50)]
        public string EtBezeichnung { get; set; }
        [Column("et_Beschreibung", TypeName = "text")]
        public string EtBeschreibung { get; set; }

        [Required]
        [Column("kd_Name")]
        [StringLength(50)]
        public string KdName { get; set; }

        [Required]
        [Column("kd_Vorname")]
        [StringLength(50)]
        public string KdVorname { get; set; }

    }
}
