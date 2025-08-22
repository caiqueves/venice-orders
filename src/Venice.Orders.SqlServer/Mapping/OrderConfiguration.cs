using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venice.Orders.Domain.Entity;

namespace enice.Orders.Infrastructure.Persistence.SqlServer.Mapping;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");               // Nome da tabela
        builder.HasKey(p => p.Id);               // Chave primária

        builder.Property(p => p.CustomerId)
         .IsRequired();

        builder.Property(p => p.Date)
         .IsRequired();

        builder.Property(p => p.Status)
         .HasConversion<int>();

        builder.Property(p => p.Total)
        .HasColumnType("decimal(18,2)");
    }
}
