using Microsoft.EntityFrameworkCore;
using Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Api.Core.Repositories
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        #region DBSets
        public DbSet<Compania> Companias { get; set; }

        public DbSet<Licencia> Licencias { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        
        public DbSet<CondicionPago> CondicionesPago { get; set; }

        public DbSet<Provincia> Provincias { get; set; }

        public DbSet<Localidad> Localidades { get; set; }

        public DbSet<ConceptoCliente> ConceptosClientes { get; set; }

        public DbSet<Liquidacion> Liquidaciones { get; set; }

        public DbSet<DetalleLiquidacionPre> DetalleLiquidacionPre { get; set; }

        public DbSet<DetalleLiquidacionPos> DetalleLiquidacionPos { get; set; }

        public DbSet<ConceptoLiquidacion> ConceptosLiquidaciones { get; set; }

        public DbSet<IngresosBrutosArchivo> IngresosBrutosArchivos { get; set; }

        public DbSet<Comprobante> Comprobantes { get; set; }

        public DbSet<ErpMilongaIdentificationType> ErpMilongaIdentificationTypeDbSet { get; set; }

        public DbSet<ErpMilongaProductType> ErpMilongaProductTypeDbSet { get; set; }

        public DbSet<ErpMilongaTaxType> ErpMilongaTaxTypeDbSet { get; set; }

        public DbSet<Impuesto> Impuestos { get; set; }

        public DbSet<ErpMilongaTaxCode> ErpMilongaTaxCodeDbSet { get; set; }

        public DbSet<ErpMilongaUnitOfMeasure> ErpMilongaUnitOfMeasureDbSet { get; set; }

        public DbSet<ErpMilongaProductCode> ErpMilongaProductCodes { get; set; }

        public DbSet<ClienteImpuesto> ClientesImpuestos { get; set; }

        public DbSet<LiquidacionPago> LiquidacionPago { get; set; }

        public DbSet<ErpInvoiceSyncLog> ErpInvoiceSyncLogs { get; set; }

        public DbSet<OmsSyncLog> OmsSyncLogs { get; set; }


        #endregion
    }
}
