using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter2.ViewModels
{
    public class PublicacionViewModel
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string HoraPublicacion { get; set; }
        public string Texto { get; set; }

        public string Foto{ get; set; }

    }
}
