using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
    public class LiquidacionConfiguration : IEntityTypeConfiguration<Liquidacion>
	{
		public void Configure(EntityTypeBuilder<Liquidacion> builder)
		{
			builder
				.HasOne(x => x.Cliente)
				.WithMany()
				.HasForeignKey(x => x.IdCliente)
				.OnDelete(DeleteBehavior.NoAction);
			
		}
	}
}
