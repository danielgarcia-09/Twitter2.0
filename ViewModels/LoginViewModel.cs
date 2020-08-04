using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter2.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage ="Este campo es requerido")]
        public string User { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
    }
}
