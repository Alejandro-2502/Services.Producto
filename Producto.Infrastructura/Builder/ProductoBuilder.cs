using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Producto.Domain.Entitys;

namespace Producto.Infrastructura.Builder;

public class ProductoBuilder : IEntityTypeConfiguration<ProductoEntity>
{
    public void Configure(EntityTypeBuilder<ProductoEntity> entity)
    {
        entity.ToTable("PRODUCTO");
        entity.Property(e => e.Nombre)
           .HasMaxLength(30);
        entity.Property(e => e.Stock);
        entity.Property(e => e.Precio)
                .HasPrecision(18, 2);
        entity.Property(e => e.FechaAlta);
    }
}
