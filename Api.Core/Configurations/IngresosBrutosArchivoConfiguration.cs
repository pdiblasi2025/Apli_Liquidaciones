using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Configurations
{
    public class IngresosBrutosArchivoConfiguration : IEntityTypeConfiguration<IngresosBrutosArchivo>
	{
		public void Configure(EntityTypeBuilder<IngresosBrutosArchivo> builder)
		{
            builder
                .HasOne(x => x.Cliente)
                .WithMany(y => y.IngresosBrutosArchivos)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
	}
}
