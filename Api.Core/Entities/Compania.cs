using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Entities
{
    public class Compania : EntityBase<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string NombreUsuario { get; set; }

        public string ApellidoUsuario { get; set; }

        //public int? UsuarioId { get; set; }
        //[ForeignKey("UsuarioId")]
        //public Usuario Usuario { get; set; }

        public string AuthId { get; set; }

        public virtual IList<Licencia> Licencias { get; set; }
        
        [NotMapped]
        public Licencia LicenciaActiva { get; set; }
    }
}
