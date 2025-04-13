using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class Role:DtoBase<int?>
    {
        public string Nombre { get; set; }


        public string Descripcion { get; set; }

        List<Permiso> Permisos { get; set; }
    }
}
