using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    [Table("tbl_Kunden", Schema = "dbo")]
    public partial class TblKunden
    {
        public TblKunden()
        {
            TblBuchungens = new HashSet<TblBuchungen>();
        }

        [Key]
        [Column("kd_KundenID")]
        public int KdKundenId { get; set; }
        [Required]
        [Column("kd_Name")]
        [StringLength(50)]
        public string KdName { get; set; }
        [Required]
        [Column("kd_Vorname")]
        [StringLength(50)]
        public string KdVorname { get; set; }
        [Required]
        [Column("kd_Strasse")]
        [StringLength(50)]
        public string KdStrasse { get; set; }
        [Required]
        [Column("kd_HNummer")]
        [StringLength(10)]
        public string KdHnummer { get; set; }
        [Required]
        [Column("kd_PLZ")]
        [StringLength(10)]
        public string KdPlz { get; set; }
        [Required]
        [Column("kd_Ort")]
        [StringLength(50)]
        public string KdOrt { get; set; }
        [Column("kd_Telefon")]
        [StringLength(25)]
        public string KdTelefon { get; set; }
        [Column("kd_EMail")]
        [StringLength(50)]
        public string KdEmail { get; set; }
        [Column("kd_PasswortHash")]
        [StringLength(255)]
        public string KdPasswortHash { get; set; }

        [InverseProperty(nameof(TblBuchungen.KdKunden))]
        public virtual ICollection<TblBuchungen> TblBuchungens { get; set; }
    }
}
