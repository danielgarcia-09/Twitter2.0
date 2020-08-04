using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS
{
    public class PublicacionDTO
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string HoraPublicacion { get; set; }
        public string Texto { get; set; }
        public string Foto { get; set; }
    }
}
