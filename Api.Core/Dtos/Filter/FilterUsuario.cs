using Api.Core.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Filter
{
    public class FilterUsuario : FilterBase
    {
        public int idUsuarioCompania { get; set; }
    }
}
