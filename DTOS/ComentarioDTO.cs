using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS
{
    public class ComentarioDTO
    {
        public int Id { get; set; }
        public int IdPost { get; set; }

        public string UserComm { get; set; }
        public string Foto { get; set; }
        public string Comentario { get; set; }
    }
}
