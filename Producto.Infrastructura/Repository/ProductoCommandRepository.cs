using Producto.Domain.Entitys;
using Producto.Domain.Interfaces;
using Producto.Infrastructura.Context;

namespace Producto.Infrastructura.Repository;

public class ProductoCommandRepository : IProductoCommandRepository
{
    private readonly DataContext _dataContext;

    public ProductoCommandRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ProductoEntity> AddAsync(ProductoEntity productoEntity)
    {
        var result = await _dataContext.Producto.AddAsync(productoEntity);
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(ProductoEntity productoEntity)
    {
        _dataContext.Producto.Remove(productoEntity);
        return true;
    }

    public async Task<ProductoEntity> UpdateAsync(ProductoEntity productoEntity)
    {
        var result = _dataContext.Producto.Update(productoEntity);
        return result.Entity;
    }
}
