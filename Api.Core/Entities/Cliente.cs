using Api.Core.Enums;
using Api.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.Entities
{
    public class Cliente : EntityBase<int>
    {
        public int OmsId { get; set; }
        public string NombreUsuario { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string RazonSocialNombre { get => !string.IsNullOrEmpty(RazonSocial) ? RazonSocial : Nombre; }

        public string EstadosFacturacion
        {
            get
            {
                var estadosFacturacion = GetEstadosFacturacion();

                return estadosFacturacion.Any() ? string.Join(", ", estadosFacturacion.Select(x => EnumHelper.GetDescription((EstadoFacturacion)x))) : string.Empty;
            }
        }
        public string NumeroDeDocumento { get; set; }
        public TipoCliente? TipoCliente { get; set; }
        public int? CondicionPagoId { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }     
        public string Calle { get; set; }
        public int? Altura { get; set; }
        public string Depto { get; set; }
        public int? LocalidadId { get; set; }
        public string CodigoPostal { get; set; }
        public Localidad Localidad { get; set; }
        public string MetodosEnvio { get; set; }
        public bool EnvioPorEmail { get; set; }
        public string NumeroIngresosBrutos { get; set; }
        public CondicionPago CondicionPago { get; set; }
        public int? TipoImpuestoId { get; set; }
        public ErpMilongaTaxType TipoImpuesto { get; set; }
        public int? TipoDocumentoId { get; set; }
        public ErpMilongaIdentificationType TipoDocumento { get; set; }
        public IList<ConceptoCliente> Conceptos { get; set; } = new List<ConceptoCliente>();
        public IList<IngresosBrutosArchivo> IngresosBrutosArchivos { get; set; } = new List<IngresosBrutosArchivo>();
        public IList<ClienteImpuesto> Impuestos { get; set; } = new List<ClienteImpuesto>();

        public List<MetodoDeEnvio> GetMetodosEnvio()
        {
            return !string.IsNullOrEmpty(MetodosEnvio) ? JsonConvert.DeserializeObject<List<MetodoDeEnvio>>(MetodosEnvio) : new List<MetodoDeEnvio>();
        }
        
        public List<int> GetEstadosFacturacion()
        {
            var metodosEnvio = GetMetodosEnvio();
            var estadosFacturacion = new List<int>();

            foreach (var metodoEnvio in metodosEnvio)
            {
                if (metodoEnvio.EstadosFacturacion.Length > 0)
                    estadosFacturacion.AddRange(metodoEnvio.EstadosFacturacion.ToList());
            }

            return estadosFacturacion.Distinct().ToList();
        }
    }

    public class MetodoDeEnvio
    {
        public int MetodoEnvio { get; set; }
        public int[] EstadosFacturacion { get; set; }
    }
}
