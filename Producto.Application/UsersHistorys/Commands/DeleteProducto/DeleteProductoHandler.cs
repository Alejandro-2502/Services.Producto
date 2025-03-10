using AutoMapper;
using MediatR;
using Producto.Application.Common.Logger;
using Producto.Application.Common.Response;
using Producto.Application.Generics;
using Producto.Application.Requests;
using Producto.Application.Responses;
using Producto.Domain.Entitys;
using Producto.Domain.Interfaces;
using System.Net;
using static Producto.Application.Enums.LoggerTypes;
using static Producto.Application.UsersHistorys.Commands.DeleteProducto.DeleteProductoHandler;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;

namespace Producto.Application.UsersHistorys.Commands.DeleteProducto;

public class DeleteProductoHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
    : IRequestHandler<DeleteProductoCommand, Responses<ProductoResponse>>

{
    public record DeleteProductoCommand(ProductoRequest clienteRequest) : IRequest<Responses<ProductoResponse>>;
    public async Task<Responses<ProductoResponse>> Handle(DeleteProductoCommand command, CancellationToken cancellationToken)
    {
        try
        {

            var productoEntity = _mapper.Map<ProductoEntity>(command.clienteRequest);

            var result = await _unitOfWork.ProductoCommandRepository.DeleteAsync(productoEntity);

            if (result is false)
                return await Response.Error<ProductoResponse>(HttpStatusCode.Conflict, Messages.MessagesProducto.CreateProductoConflict);

            await _unitOfWork.SaveChangesAsync();

            await _mediator.Send(new RegisterLogCommand(
                    new LogsRegister { Type = LoggerType.Information, Messages = nameof(DeleteProductoHandler) + " - " + nameof(Handle) + "- OK" }), cancellationToken);

            return await Response.Ok<ProductoResponse>(HttpStatusCode.OK, Messages.MessagesProducto.DeleteProductoOk);
        }
        catch (Exception ex)
        {
            await _mediator.Send(new RegisterLogCommand(
                  new LogsRegister { Type = LoggerType.Error, Messages = nameof(DeleteProductoHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);
            return await Response.Error<ProductoResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

}
