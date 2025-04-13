using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class Compania : DtoBase<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public Usuario Usuario { get; set; }
        //public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; }

        public string ApellidoUsuario { get; set; }
        public string AuthId { get; set; }
        public Licencia LicenciaActiva { get; set; }
    }
}
