using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
	{
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
			builder
				.HasOne(x => x.CondicionPago)
				.WithMany()
				.HasForeignKey(x => x.CondicionPagoId)
				.OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Localidad)
                .WithMany()
                .HasForeignKey(x => x.LocalidadId)
                .OnDelete(DeleteBehavior.NoAction);

			builder.HasIndex(x => x.OmsId).HasName("IX_Clientes_OmsId");
		}

	}
}
