using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Model
{
    public class Replies
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public int IdComentario { get; set; }
        public string Foto { get; set; }
        public string HoraPublicacion { get; set; }
        public string Texto { get; set; }

    }
}
