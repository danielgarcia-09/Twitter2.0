using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.AspNetCore.Http;

namespace Twitter2.ViewModels
{
    public class THomeViewModel
    {
        public string Usuario { get; set; }
        public string FotoUsuario { get; set; }
        public IEnumerable<Publicaciones> Publicaciones { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Amigos> Amigos { get; set; }


        public string TextoPublicacion { get; set; }
        public IFormFile FotoPublicacion { get; set; }


    }
}
