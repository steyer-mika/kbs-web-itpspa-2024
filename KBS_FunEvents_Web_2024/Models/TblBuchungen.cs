using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    [Table("tbl_Buchungen", Schema = "dbo")]
    public partial class TblBuchungen
    {
        [Key]
        [Column("bu_BuchungsID")]
        public int BuBuchungsId { get; set; }
        [Column("kd_KundenID")]
        public int KdKundenId { get; set; }
        [Column("ed_EvDatenID")]
        public int EdEvDatenId { get; set; }
        [Column("bu_GebuchtePlaetze")]
        public int BuGebuchtePlaetze { get; set; }
        [Column("bu_Bezahlt")]
        public bool BuBezahlt { get; set; }
        [Column("bu_Storniert")]
        public bool BuStorniert { get; set; }
        [Column("bu_RechnungErstellt")]
        public bool BuRechnungErstellt { get; set; }

        [ForeignKey(nameof(EdEvDatenId))]
        [InverseProperty(nameof(TblEventDaten.TblBuchungens))]
        public virtual TblEventDaten EdEvDaten { get; set; }
        [ForeignKey(nameof(KdKundenId))]
        [InverseProperty(nameof(TblKunden.TblBuchungens))]
        public virtual TblKunden KdKunden { get; set; }
    }
}
