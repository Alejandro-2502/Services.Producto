using AutoMapper;
using MediatR;
using Producto.Application.Common.Logger;
using Producto.Application.Common.Response;
using Producto.Application.Generics;
using Producto.Application.Responses;
using Producto.Domain.Interfaces;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;
using System.Net;
using static Producto.Application.UsersHistorys.Querys.GetByNameProducto.GetNameProductoHandler;
using static Producto.Application.Enums.LoggerTypes;

namespace Producto.Application.UsersHistorys.Querys.GetByNameProducto;

public class GetNameProductoHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
    : IRequestHandler<GetByNameProductoQuery, Responses<List<ProductoResponse>>>
{
    public record GetByNameProductoQuery(string name) : IRequest<Responses<List<ProductoResponse>>>;
    public async Task<Responses<List<ProductoResponse>>> Handle(GetByNameProductoQuery command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.ProductoQuerysRepository.GetByNameAsync(command.name);

            if (result.Count == 0)
                return await Response.Error<List<ProductoResponse>>(HttpStatusCode.NotFound, Messages.MessagesProducto.GetAllProductoNotFound);

            var response = _mapper.Map<List<ProductoResponse>>(result);

            return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
        }
        catch (Exception ex)
        {
            await _mediator.Send(new RegisterLogCommand(
               new LogsRegister { Type = LoggerType.Error, Messages = nameof(GetByNameProductoQuery).ToString() + " - " + nameof(Handle).ToString() + ex.ToString() }), cancellationToken);
            return await Response.Error<List<ProductoResponse>>(HttpStatusCode.InternalServerError, ex.Message);

        }
    }
}
