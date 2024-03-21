using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    [Table("tbl_EvKategorie", Schema = "dbo")]
    public partial class TblEvKategorie
    {
        public TblEvKategorie()
        {
            TblEvents = new HashSet<TblEvent>();
        }

        [Key]
        [Column("ek_EvKategorieID")]
        public int EkEvKategorieId { get; set; }
        [Required]
        [Column("ek_KatBezeichnung")]
        [StringLength(15)]
        public string EkKatBezeichnung { get; set; }

        [InverseProperty(nameof(TblEvent.EkEvKategorie))]
        public virtual ICollection<TblEvent> TblEvents { get; set; }
    }
}
