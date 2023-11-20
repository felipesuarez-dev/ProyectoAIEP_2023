using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Menu
    {
        public int IdMenu { get; set; }
        public string NombreMenu { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool BolActivo { get; set; }
        public int IdTipoMenu { get; set; }
        public int IdMenuPrincipal { get; set; }
    }
}
