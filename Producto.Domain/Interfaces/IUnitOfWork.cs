namespace Producto.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IProductoCommandRepository ProductoCommandRepository { get; }
    IProductoQuerysRepository ProductoQuerysRepository { get; }
}
