using System;
using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoCidadeMap : IEntityTypeConfiguration<RegiaoCidade>
    {
        public void Configure(EntityTypeBuilder<RegiaoCidade> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne<Regiao>()
                .WithMany(r => r.RegiaoCidades)
                .HasForeignKey(rc => rc.RegiaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Cidade>()
                .WithOne()
                .HasForeignKey<RegiaoCidade>(rc => rc.CidadeId)
                .OnDelete(DeleteBehavior.Cascade);          
        }
    }
}
