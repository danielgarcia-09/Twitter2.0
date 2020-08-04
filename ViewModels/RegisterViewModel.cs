using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter2.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Apellido { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Telefono { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "No coinciden las contraseñas")]
        public string ConfirmPassword { get; set; }

        public string Estado { get; set; }

        public IFormFile Photo { get; set; }

        public string Foto { get; set; }
    }
}
