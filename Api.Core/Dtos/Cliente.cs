using Api.Core.Enums;
using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class Cliente : DtoBase<int?>
    {
        public string OmsId { get; set; }
        public string NombreUsuario { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string EstadosFacturacion { get; set; }
        public string RazonSocialNombre { get; set; }
        public string NumeroDeDocumento { get; set; }
        public TipoCliente? TipoCliente { get; set; }
        public CondicionPago CondicionPago { get; set; }
        public int? CondicionPagoId { get; set; }
        public List<MetodoDeEnvio> MetodosEnvio { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Calle { get; set; }
        public int? Altura { get; set; }
        public string Depto { get; set; }
        public Localidad Localidad { get; set; }
        public int? LocalidadId { get; set; }
        public string CodigoPostal { get; set; }
        public int? ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        public bool EnvioPorEmail { get; set; }
        public string NumeroIngresosBrutos { get; set; }
        public int? TipoImpuestoId { get; set; }
        public TipoImpuesto TipoImpuesto { get; set; }
        public int? TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public IList<IngresosBrutosArchivo> IngresosBrutosArchivos { get; set; }
        public IList<ClienteImpuesto> Impuestos { get; set; }
    }

    public class MetodoDeEnvio
    {
        public int MetodoEnvio { get; set; }
        public int[] EstadosFacturacion { get; set; }
    }
}
