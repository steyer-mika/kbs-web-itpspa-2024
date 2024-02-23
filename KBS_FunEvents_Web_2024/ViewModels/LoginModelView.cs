
using System.ComponentModel.DataAnnotations;

namespace KBS_FunEvents_Web_2024.ViewModels
{
    public class LoginModelView
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Email*")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string KdEmail { get; set; }

        [Required(ErrorMessage = "Passwor*")]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string KdPwHash { get; set; }
    }
}
