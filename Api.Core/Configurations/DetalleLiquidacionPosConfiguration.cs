using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Configurations
{
    public class DetalleLiquidacionPosConfiguration : IEntityTypeConfiguration<DetalleLiquidacionPos>
	{
		public void Configure(EntityTypeBuilder<DetalleLiquidacionPos> builder)
		{
			builder
				.HasOne(x => x.Liquidacion)
				.WithMany(x => x.DetalleLiquidacionPos)
				.HasForeignKey(x => x.LiquidacionId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasIndex(x => x.OmsId).HasName("IX_DetalleLiquidacionPos_OmsId");
		}
	}
}
