using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
    public class ComprobanteConfiguration : IEntityTypeConfiguration<Comprobante>
    {
        public void Configure(EntityTypeBuilder<Comprobante> builder)
        {
            builder
                .HasOne(x => x.Liquidacion)
                .WithMany(y => y.Comprobantes)
                .HasForeignKey(x => x.LiquidacionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
