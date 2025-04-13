using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class Plan : DtoBase<int?>
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? CantidadUsuarios { get; set; }
        public int CantidadEmprendimientos { get; set; }
        public int MesesValidez { get; set; }
    }
}
