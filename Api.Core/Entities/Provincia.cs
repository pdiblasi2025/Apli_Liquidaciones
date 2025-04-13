using System.Collections.Generic;

namespace Api.Core.Entities
{
    public class Provincia
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public IList<Localidad> Localidades { get; set; } = new List<Localidad>();
    }
}
