using AutoMapper;
using Producto.Application.Requests;
using Producto.Application.Responses;
using Producto.Domain.Entitys;

namespace Producto.Application.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ProductoEntity, ProductoRequest>();
        CreateMap<ProductoRequest, ProductoEntity>();
        CreateMap<ProductoEntity, ProductoResponse>();
    }
}
