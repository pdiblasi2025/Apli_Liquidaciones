using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class Licencia : DtoBase<int?>
    {
        public string Codigo { get; set; }
        public DateTime Expiracion { get; set; }
        public int Estado { get; set; }
        public int CompaniaId { get; set; }
        public int PlanId { get; set; }
        public string CompaniaNombre { get; set; }
        public Plan Plan { get; set; }
        public string Descripcion { get; set; }
    }
}
