using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Request
{
    public class GeneraLiquidacionEnviosRequest
    {
        public string descripcion { get; set; }
        public int[] ids_envios { get; set; }
    }
}
