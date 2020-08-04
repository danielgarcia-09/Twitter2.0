using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class Usuario
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Foto { get; set; }
        public string Estado { get; set; }
    }
}
