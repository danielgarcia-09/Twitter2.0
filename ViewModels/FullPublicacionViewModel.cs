using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter2.ViewModels
{
    public class FullPublicacionViewModel
    {
        public Publicaciones Publicacion { get; set; }
         public IEnumerable<Comentarios> Comentarios { get; set; }
        public IEnumerable<Replies> Replies { get; set; }
        public string Comentario { get; set; }
        public string Reply { get; set; }
    }
}
