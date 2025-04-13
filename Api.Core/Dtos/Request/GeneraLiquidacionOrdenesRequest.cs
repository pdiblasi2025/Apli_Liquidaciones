using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Request
{
    public class GeneraLiquidacionOrdenesRequest
    {
        public string descripcion { get; set; }
        public int[] ids_ordenes { get; set; }
    }
}
