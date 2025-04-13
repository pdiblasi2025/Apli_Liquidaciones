using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class CondicionPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ErpId { get; set; }
    }
}
