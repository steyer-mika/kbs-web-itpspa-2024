
using System.ComponentModel.DataAnnotations;

namespace KBS_FunEvents_Web_2024.ViewModels
{
    public class RegistrationModelView
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Nachname*")]
        [Display(Name = "Nachname")]
        [DataType(DataType.Text)]
        public string Nachname { get; set; }
        [Required(ErrorMessage = "Vorname*")]
        [Display(Name = "Vorname")]
        [DataType(DataType.Text)]
        public string Vorname { get; set; }
        [Required(ErrorMessage = "Strasse*")]
        [Display(Name = "Strasse")]
        [DataType(DataType.Text)]
        public string Strasse { get; set; }
        [Required(ErrorMessage = "Hausnummer*")]
        [Display(Name = "Hausnummer")]
        [DataType(DataType.Text)]
        public string Hausnummer { get; set; }
        [Required(ErrorMessage = "Postleitzahl*")]
        [Display(Name = "Postleitzahl")]
        [DataType(DataType.PostalCode)]
        public string Postleitzahl { get; set; }
        [Required(ErrorMessage = "Ort*")]
        [Display(Name = "Ort")]
        [DataType(DataType.Text)]
        public string Ort { get; set; }
        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Email*")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string KdEmail { get; set; }

        [Required(ErrorMessage = "Passwort*")]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        [Compare("Passwort", ErrorMessage ="Passwörter sind nicht gleich!")]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string PasswortWdh { get; set; }


    }
}
