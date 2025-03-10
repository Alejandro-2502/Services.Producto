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
using static Producto.Application.UsersHistorys.Commands.CreateProducto.CreateProductoHandler;
using static Producto.Application.UsersHistorys.Commands.CreateProducto.CreateValidationsProductoHandler;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;

namespace Producto.Application.UsersHistorys.Commands.CreateProducto;

public class CreateProductoHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
    :IRequestHandler<CreateProductoCommand, Responses<ProductoResponse>>
{
    public record CreateProductoCommand(ProductoRequest productoRequest) : IRequest<Responses<ProductoResponse>>;
    public async Task<Responses<ProductoResponse>> Handle(CreateProductoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var resultValidator = await _mediator.Send(new ValidatorCreateProducto(command.productoRequest), cancellationToken);

            if (resultValidator is not null)
                return resultValidator;

            var productoEntity = _mapper.Map<ProductoEntity>(command.productoRequest);
            var result = await _unitOfWork.ProductoCommandRepository.AddAsync(productoEntity);

            if (result is null)
                return await Response.Error<ProductoResponse>(HttpStatusCode.Conflict, Messages.MessagesProducto.CreateProductoConflict);

            await _unitOfWork.SaveChangesAsync();

            await _mediator.Send(new RegisterLogCommand(
                  new LogsRegister { Type = LoggerType.Information, Messages = nameof(CreateProductoHandler) + " - " + nameof(Handle) + "- OK" }), cancellationToken);

            var response = _mapper.Map<ProductoResponse>(result);

            return await Response.Ok<ProductoResponse>(HttpStatusCode.OK, Messages.MessagesProducto.CreateProductoOk);
        }
        catch (Exception ex)
        {
            await _mediator.Send(new RegisterLogCommand(
                new LogsRegister { Type = LoggerType.Error, Messages = nameof(CreateProductoCommand) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

            return await Response.Error<ProductoResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }   
    }
}
