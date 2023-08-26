using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalGalaxy.Entities;

namespace PortalGalaxy.DataAccess.Configurations
{
    public class TallerConfiguration : IEntityTypeConfiguration<Taller>
    {
        public void Configure(EntityTypeBuilder<Taller> builder)
        {
            builder.Property(p => p.Nombre)
                .HasMaxLength(200);

            builder.Property(p => p.FechaInicio)
                .HasColumnType("DATE");

            builder.Property(p => p.HoraInicio)
                .HasColumnType("DATETIME");

            builder.Property(p => p.PortadaUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(p => p.TemarioUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            // Las tablas temporales o historicas son solo compatibles con SQL Server
            builder.ToTable(nameof(Taller), p => p.IsTemporal());
        }
    }
}
