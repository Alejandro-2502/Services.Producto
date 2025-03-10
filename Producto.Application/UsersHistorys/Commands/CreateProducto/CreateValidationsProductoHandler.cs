using MediatR;
using Producto.Application.Common.Logger;
using Producto.Application.Common.Response;
using Producto.Application.Generics;
using Producto.Application.Requests;
using Producto.Application.Responses;
using System.Net;
using static Producto.Application.Enums.LoggerTypes;
using static Producto.Application.UsersHistorys.Commands.CreateProducto.CreateValidationsProductoHandler;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;

namespace Producto.Application.UsersHistorys.Commands.CreateProducto
{
    public class CreateValidationsProductoHandler (CreateProductoValidator _createProductoValidator, IMediator _mediator)
       : IRequestHandler<ValidatorCreateProducto, Responses<ProductoResponse>>
        {
            public record ValidatorCreateProducto(ProductoRequest productoRequest) : IRequest<Responses<ProductoResponse>>;
            public async Task<Responses<ProductoResponse>> Handle(ValidatorCreateProducto command, CancellationToken cancellationToken)
            {
                try
                {
                    List<string> Messages = new();
                    var result = await _createProductoValidator.ValidateAsync(command.productoRequest);

                    if (!result.IsValid)
                    {
                        Messages.AddRange(result.Errors.Select(error => error.ErrorMessage));
                        return Response.ErrorsList<ProductoResponse>(HttpStatusCode.BadRequest, Messages).Result;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                await _mediator.Send(new RegisterLogCommand(
                    new LogsRegister { Type = LoggerType.Error, Messages = nameof(CreateValidationsProductoHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

                return await Response.Error<ProductoResponse>(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }
    }

