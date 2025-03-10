using Producto.Domain.Entitys;

namespace Producto.Domain.Interfaces;

public interface IProductoCommandRepository
{
    Task<ProductoEntity> AddAsync(ProductoEntity productoEntity);
    Task<ProductoEntity> UpdateAsync(ProductoEntity productoEntity);
    Task<bool> DeleteAsync(ProductoEntity productoEntity);
}
