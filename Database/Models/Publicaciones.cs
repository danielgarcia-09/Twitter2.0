using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class Publicaciones
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string HoraPublicacion { get; set; }
        public string Texto { get; set; }
        public string Foto { get; set; }
    }
}
