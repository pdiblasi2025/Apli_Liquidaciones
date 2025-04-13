using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
	public class LiquidacionPagoConfiguration : IEntityTypeConfiguration<LiquidacionPago>
	{
		public void Configure(EntityTypeBuilder<LiquidacionPago> builder)
		{
			builder
				.HasOne(x => x.Liquidacion)
				.WithMany()
				.HasForeignKey(x => x.IdLiquidacion)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
