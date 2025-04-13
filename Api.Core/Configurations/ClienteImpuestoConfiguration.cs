using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
    public class ClienteImpuestoConfiguration : IEntityTypeConfiguration<ClienteImpuesto>
    {
        public void Configure(EntityTypeBuilder<ClienteImpuesto> builder)
        {
            builder
                .HasOne(x => x.Cliente)
                .WithMany(y => y.Impuestos)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Impuesto)
                .WithMany()
                .HasForeignKey(x => x.ImpuestoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.PorcentajeExencion).HasColumnType("decimal(5, 2)");
        }
    }
}
