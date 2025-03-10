using Microsoft.EntityFrameworkCore;
using Producto.Application.Configurations;
using Producto.Domain.Entitys;
using Producto.Infrastructura.Builder;

namespace Producto.Infrastructura.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ProductoEntity> Producto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(ConfigHelper.ConfigSqlServer!.Connection);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductoBuilder());
    }
}
