using Microsoft.EntityFrameworkCore;
using Producto.Domain.Entitys;
using Producto.Domain.Interfaces;
using Producto.Infrastructura.Context;

namespace Producto.Infrastructura.Repository
{
    public class ProductoQuerysRepository : IProductoQuerysRepository
    {
        private readonly DataContext _dataContext;

        public ProductoQuerysRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<ProductoEntity>> GetAllAsync()
        {
            try
            {
                return await _dataContext.Producto.ToListAsync();
            }
            catch (Exception) {throw;}
        }

        public async Task<ProductoEntity> GetByIdAsync(int id)
        {
            try
            {
                var result = await _dataContext.Producto
                               .Where(pro => pro.Id == id).FirstOrDefaultAsync();

                return result!;
            }
            catch (Exception) {throw;}
        }

        public async Task<List<ProductoEntity>> GetByNameAsync(string name)
        {
            try
            {
                var result = await _dataContext.Producto
                .Where(pro => EF.Functions.Like(pro.Nombre, name))
                .ToListAsync();

                return result;
            }
            catch (Exception) {throw;}
        }

        public async Task<List<ProductoEntity>> GetByPrecioMoreThanAsync(decimal precio)
        {
            try
            {
                var result = await _dataContext.Producto
                .Where(pro => pro.Precio >= precio)
                .OrderByDescending(pro => pro.Id)
                .ToListAsync();
                return result;
            }
            catch (Exception) {throw;}
        }
    }
}
