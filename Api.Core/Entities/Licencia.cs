using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Entities
{
    public class Licencia : EntityBase<int>
    {
        public string Codigo { get; set; }
        public DateTime Expiracion { get; set; }
        public int Estado { get; set; }

        public int CompaniaId { get; set; }
        [ForeignKey("CompaniaId")]
        public Compania Compania { get; set; }

        [NotMapped]
        public string CompaniaNombre { get; set; }

        public string Descripcion { get; set; }
    }
}
