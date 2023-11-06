using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public const int Activo = 1;
        public const int Bloqueado= 2;

        public int IdUsuario { get; set; }
        public string Rut { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int IdRol { get; set; }
        public int IntentosFallidos { get; set; }
        public int IdEstado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool bolActivo { get; set; }
    }
}
