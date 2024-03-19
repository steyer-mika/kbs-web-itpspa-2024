using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KBS_FunEvents_Web_2024.ViewModels
{
    public class ChangePasswordModelView
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Passwort*")]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        [Required(ErrorMessage = "PasswortWDH*")]
        [Display(Name = "PasswortWDH")]
        [DataType(DataType.Password)]
        public string PasswortWDH { get; set; }
    }
}