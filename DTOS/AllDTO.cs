using Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS
{
    public class AllDTO
    {
        public IEnumerable<Publicaciones> Publicacion { get; set; }
        public IEnumerable<Comentarios> Comentarios { get; set; }
        public IEnumerable<Replies> Replies { get; set; }
    }
}
