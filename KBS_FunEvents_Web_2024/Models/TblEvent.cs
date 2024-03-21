using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    [Table("tbl_Events", Schema = "dbo")]
    public partial class TblEvent
    {
        public TblEvent()
        {
            TblEventDatens = new HashSet<TblEventDaten>();
        }

        [Key]
        [Column("et_EventID")]
        public int EtEventId { get; set; }
        [Column("ev_EvVeranstalterID")]
        public int EvEvVeranstalterId { get; set; }
        [Column("ek_EvKategorieID")]
        public int EkEvKategorieId { get; set; }
        [Required]
        [Column("et_Bezeichnung")]
        [StringLength(50)]
        public string EtBezeichnung { get; set; }
        [Column("et_Beschreibung", TypeName = "text")]
        public string EtBeschreibung { get; set; }

        [ForeignKey(nameof(EkEvKategorieId))]
        [InverseProperty(nameof(TblEvKategorie.TblEvents))]
        public virtual TblEvKategorie EkEvKategorie { get; set; }
        [ForeignKey(nameof(EvEvVeranstalterId))]
        [InverseProperty(nameof(TblEvVeranstalter.TblEvents))]
        public virtual TblEvVeranstalter EvEvVeranstalter { get; set; }
        [InverseProperty(nameof(TblEventDaten.EtEvent))]
        public virtual ICollection<TblEventDaten> TblEventDatens { get; set; }
    }
}
