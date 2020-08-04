using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Model
{
    public partial class Comentarios
    {
        public int Id { get; set; }
        public int IdPost { get; set; }

        public string UserComm { get; set; }
        public string Foto { get; set; }
        public string Comentario { get; set; }
    }
}
