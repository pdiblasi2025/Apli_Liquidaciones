using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Entities
{
    public class Localidad
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int ProvinciaId { get; set; }

        public Provincia Provincia { get; set; }
    }
}
