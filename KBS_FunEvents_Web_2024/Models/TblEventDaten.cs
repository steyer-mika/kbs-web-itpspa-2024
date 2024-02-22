using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    [Table("tbl_EventDaten", Schema = "dbo")]
    public partial class TblEventDaten
    {
        public TblEventDaten()
        {
            TblBuchungens = new HashSet<TblBuchungen>();
        }

        [Key]
        [Column("ed_EvDatenID")]
        public int EdEvDatenId { get; set; }
        [Column("et_EventID")]
        public int EtEventId { get; set; }
        [Column("ed_Preis", TypeName = "smallmoney")]
        public decimal EdPreis { get; set; }
        [Column("ed_Beginn", TypeName = "datetime")]
        public DateTime EdBeginn { get; set; }
        [Column("ed_Ende", TypeName = "datetime")]
        public DateTime EdEnde { get; set; }
        [Required]
        [Column("ed_StartOrt")]
        [StringLength(50)]
        public string EdStartOrt { get; set; }
        [Column("ed_Zielort")]
        [StringLength(50)]
        public string EdZielort { get; set; }
        [Column("ed_MaxTeilnehmer")]
        public int EdMaxTeilnehmer { get; set; }
        [Column("ed_AktTeilnehmer")]
        public int EdAktTeilnehmer { get; set; }
        [Column("ed_Freigegeben")]
        public bool EdFreigegeben { get; set; }
        [Column("ed_Rabatt", TypeName = "decimal(18, 0)")]
        public decimal EdRabatt { get; set; }
        [Column("ed_VeranstalterBenachrichtigt")]
        public bool EdVeranstalterBenachrichtigt { get; set; }

        [ForeignKey(nameof(EtEventId))]
        [InverseProperty(nameof(TblEvent.TblEventDatens))]
        public virtual TblEvent EtEvent { get; set; }
        [InverseProperty(nameof(TblBuchungen.EdEvDaten))]
        public virtual ICollection<TblBuchungen> TblBuchungens { get; set; }
    }
}
