using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class Permiso : DtoBase<int?>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
