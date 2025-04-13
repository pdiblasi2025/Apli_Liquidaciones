using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Request
{
    public class OrdenesyEnviosRequest
    {
        public string numerodocumento { get; set; }
        public int tipocliente { get; set; }
    }
}
