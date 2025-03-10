using AutoMapper;
using MediatR;
using Producto.Application.Common.Logger;
using Producto.Application.Common.Response;
using Producto.Application.Generics;
using Producto.Application.Responses;
using Producto.Domain.Interfaces;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;
using System.Net;
using static Producto.Application.UsersHistorys.Querys.GetByIdProducto.GetByIdProductoHandler;
using static Producto.Application.Enums.LoggerTypes;

namespace Producto.Application.UsersHistorys.Querys.GetByIdProducto
{
    public class GetByIdProductoHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetByIdProductoQuery, Responses<ProductoResponse>>
    {
        public record GetByIdProductoQuery(int id) : IRequest<Responses<ProductoResponse>>;
        public async Task<Responses<ProductoResponse>> Handle(GetByIdProductoQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.ProductoQuerysRepository.GetByIdAsync(command.id);

                if (result is null)
                    return await Response.Error<ProductoResponse>(HttpStatusCode.NotFound, Messages.MessagesProducto.GetByIdProductoNotFound);

                var response = _mapper.Map<ProductoResponse>(result);

                return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new RegisterLogCommand(
                   new LogsRegister { Type = LoggerType.Error, Messages = nameof(GetByIdProductoHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

                return await Response.Error<ProductoResponse>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
