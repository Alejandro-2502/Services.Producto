using Producto.Domain.Interfaces;
using Producto.Infrastructura.Context;
using Producto.Infrastructura.Repository;

namespace Producto.Infrastructura.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IProductoCommandRepository _productoCommandRepository;
    public IProductoQuerysRepository _productoQueryRepository;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IProductoCommandRepository ProductoCommandRepository => _productoCommandRepository =
             _productoCommandRepository ?? new ProductoCommandRepository(_dataContext);

    public IProductoQuerysRepository ProductoQuerysRepository => _productoQueryRepository =
        _productoQueryRepository ?? new ProductoQuerysRepository(_dataContext);

    public async Task<int> SaveChangesAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
    public void Dispose()
    {
        _dataContext.Dispose();
    }
}
