using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Configurations
{
    public class DetalleLiquidacionPreConfiguration : IEntityTypeConfiguration<DetalleLiquidacionPre>
	{
		public void Configure(EntityTypeBuilder<DetalleLiquidacionPre> builder)
		{
			builder
				.HasOne(x => x.Liquidacion)
				.WithMany(x => x.DetalleLiquidacionPre)
				.HasForeignKey(x => x.LiquidacionId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasIndex(x => x.OmsId).HasName("IX_DetalleLiquidacionPre_OmsId");
		}
	}
}
