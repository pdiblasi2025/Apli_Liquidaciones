using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
    public class ConceptoConfiguration : IEntityTypeConfiguration<Concepto>
    {
        public void Configure(EntityTypeBuilder<Concepto> builder)
        {
            builder.HasQueryFilter(x => x.IsGeneric);
            builder.HasQueryFilter(x => !x.Deleted);
            builder
                .HasOne(x => x.CodigoProducto)
                .WithMany()
                .HasForeignKey(x => x.CodigoProductoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new Concepto { Id = 1, Nombre = "Flete", IsGeneric = true });
        }
    }
}
