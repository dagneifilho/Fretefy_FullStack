using System;
using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings{
    public class RegiaoMap : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Nome)
                .HasMaxLength(1024)
                .IsRequired();
            builder.Property(r => r.Ativa).HasDefaultValue(true);

            builder.HasMany(r => r.RegiaoCidades)
                .WithOne()
                .HasForeignKey(rc => rc.RegiaoId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
