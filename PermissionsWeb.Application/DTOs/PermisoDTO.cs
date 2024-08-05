using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsWeb.Application.DTOs
{
    public class PermisoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha { get; set; }
        //public int TipoPermisoId { get; set; }
        public int TipoPermiso { get; set; }
    }
}
