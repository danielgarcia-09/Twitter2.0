using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS
{
    public class UserDTO
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
