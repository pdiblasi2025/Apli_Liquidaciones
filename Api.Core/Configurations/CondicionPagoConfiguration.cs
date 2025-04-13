using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Core.Configurations
{
    public class CondicionPagoConfiguration : IEntityTypeConfiguration<CondicionPago>
	{
		public void Configure(EntityTypeBuilder<CondicionPago> builder)
		{
		}
	}
}
