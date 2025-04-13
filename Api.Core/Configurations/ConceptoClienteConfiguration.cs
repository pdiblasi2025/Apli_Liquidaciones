using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Configurations
{
    public class ConceptoClienteConfiguration : IEntityTypeConfiguration<ConceptoCliente>
	{
		public void Configure(EntityTypeBuilder<ConceptoCliente> builder)
		{
            builder
                .HasOne(x => x.Concepto)
                .WithMany(y => y.Clientes)
                .HasForeignKey(x => x.ConceptoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Cliente)
                .WithMany(y => y.Conceptos)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Monto).HasColumnType("decimal(20, 2)");

            builder.HasQueryFilter(x => !x.Deleted);
		}
	}
}
