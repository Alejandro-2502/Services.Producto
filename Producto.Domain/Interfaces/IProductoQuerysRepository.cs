using Producto.Domain.Entitys;

namespace Producto.Domain.Interfaces;

public interface IProductoQuerysRepository
{
    Task<List<ProductoEntity>> GetAllAsync();
    Task<ProductoEntity> GetByIdAsync(int id);
    Task<List<ProductoEntity>> GetByNameAsync(string name);
    Task<List<ProductoEntity>> GetByPrecioMoreThanAsync(decimal precio);
}
