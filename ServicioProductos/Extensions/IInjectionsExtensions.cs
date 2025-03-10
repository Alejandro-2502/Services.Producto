using Producto.Application.UsersHistorys.Commands.CreateProducto;
using Producto.Application.UsersHistorys.Commands.DeleteProducto;
using Producto.Application.UsersHistorys.Commands.UpdateProducto;
using Producto.Application.UsersHistorys.Common;
using Producto.Domain.Interfaces;
using Producto.Infrastructura.Repository;
using Producto.Infrastructura.UnitOfWork;

namespace Producto.Services.Extensions;

public static class IInjectionsExtensions
{
    public static IServiceCollection AddInjections(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductoCommandRepository, ProductoCommandRepository>();
        services.AddScoped<IProductoQuerysRepository, ProductoQuerysRepository>();

        //Registro Mediator de cada clase donde se utiliza MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductoHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteProductoHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateProductoHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoggerHandler).Assembly));

        //Registro Mediator de cada clase de validaciones donde se utiliza MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateValidationsProductoHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateValidationsProductoHandler).Assembly));


        services.AddSingleton<CreateProductoValidator>();
        services.AddSingleton<UpdateProductoValidator>();

        services.AddScoped<ResponseHttp>();
        return services;
    }
}
