using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter2.ViewModels { 
    public class ComentarioViewModel
    {
        public int IdPost { get; set; }

        public string UserComm { get; set; }

        public string Comentario { get; set; }
        public string Foto { get; set; }
    }
}
