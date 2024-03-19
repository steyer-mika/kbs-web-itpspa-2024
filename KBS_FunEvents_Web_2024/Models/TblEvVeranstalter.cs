using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    [Table("tbl_EvVeranstalter", Schema = "dbo")]
    public partial class TblEvVeranstalter
    {
        public TblEvVeranstalter()
        {
            TblEvents = new HashSet<TblEvent>();
        }

        [Key]
        [Column("ev_EvVeranstalterID")]
        public int EvEvVeranstalterId { get; set; }
        [Required]
        [Column("ev_Firma")]
        [StringLength(50)]
        public string EvFirma { get; set; }
        [Required]
        [Column("ev_Strasse")]
        [StringLength(50)]
        public string EvStrasse { get; set; }
        [Required]
        [Column("ev_PLZ")]
        [StringLength(10)]
        public string EvPlz { get; set; }
        [Required]
        [Column("ev_HNummer")]
        [StringLength(10)]
        public string EvHnummer { get; set; }
        [Required]
        [Column("ev_Ort")]
        [StringLength(50)]
        public string EvOrt { get; set; }
        [Column("ev_Telefon")]
        [StringLength(25)]
        public string EvTelefon { get; set; }
        [Column("ev_EMail")]
        [StringLength(50)]
        public string EvEmail { get; set; }
        [Column("ev_Fax")]
        [StringLength(50)]
        public string EvFax { get; set; }

        [InverseProperty(nameof(TblEvent.EvEvVeranstalter))]
        public virtual ICollection<TblEvent> TblEvents { get; set; }
    }
}
