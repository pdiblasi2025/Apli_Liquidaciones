using Api.Core.Entities;
using Api.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
    public class ConceptoLiquidacionConfiguration : IEntityTypeConfiguration<ConceptoLiquidacion>
    {
        public void Configure(EntityTypeBuilder<ConceptoLiquidacion> builder)
        {
            builder
                .HasOne(x => x.Concepto)
                .WithMany(y => y.Liquidaciones)
                .HasForeignKey(x => x.ConceptoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Liquidacion)
                .WithMany(y => y.Conceptos)
                .HasForeignKey(x => x.LiquidacionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Monto).HasColumnType("decimal(20, 2)");

            builder.HasQueryFilter(x => !x.Deleted && x.Estado != EstadoConceptoCliente.Anulado);
        }
    }
}
