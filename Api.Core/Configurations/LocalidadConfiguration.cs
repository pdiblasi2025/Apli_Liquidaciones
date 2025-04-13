using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Configurations
{
    public class LocalidadConfiguration : IEntityTypeConfiguration<Localidad>
	{
		public void Configure(EntityTypeBuilder<Localidad> builder)
		{
			builder
				.HasOne(x => x.Provincia)
				.WithMany(y => y.Localidades)
				.HasForeignKey(x => x.ProvinciaId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
