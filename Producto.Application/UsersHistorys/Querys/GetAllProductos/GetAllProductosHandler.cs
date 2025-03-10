using AutoMapper;
using MediatR;
using Producto.Application.Common.Logger;
using Producto.Application.Common.Response;
using Producto.Application.Generics;
using Producto.Application.Responses;
using Producto.Domain.Interfaces;
using System.Net;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;
using static Producto.Application.Enums.LoggerTypes;
using static Producto.Application.UsersHistorys.Querys.GetAllProductos.GetAllProductosHandler;

namespace Producto.Application.UsersHistorys.Querys.GetAllProductos;

public class GetAllProductosHandler (IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetAllProductoQuery, Responses<List<ProductoResponse>>>
{
    public record GetAllProductoQuery() : IRequest<Responses<List<ProductoResponse>>>;
    public async Task<Responses<List<ProductoResponse>>> Handle(GetAllProductoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _unitOfWork.ProductoQuerysRepository.GetAllAsync();

            if (!results.Any())
                return await Response.Error<List<ProductoResponse>>(HttpStatusCode.NotFound, Messages.MessagesProducto.GetAllProductoNotFound);

            var response = _mapper.Map<List<ProductoResponse>>(results);
            return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
        }
        catch (Exception ex)
        {
            await _mediator.Send(new RegisterLogCommand(
                new LogsRegister { Type = LoggerType.Error, Messages = nameof(GetAllProductosHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);
            return await Response.Error<List<ProductoResponse>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
