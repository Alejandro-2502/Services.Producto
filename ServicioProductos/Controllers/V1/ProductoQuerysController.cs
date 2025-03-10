using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producto.Application.Generics;
using Producto.Application.Responses;
using static Producto.Application.UsersHistorys.Querys.GetAllProductos.GetAllProductosHandler;
using static Producto.Application.UsersHistorys.Querys.GetByIdProducto.GetByIdProductoHandler;
using static Producto.Application.UsersHistorys.Querys.GetByNameProducto.GetNameProductoHandler;

namespace Producto.Services.Controllers.V1;

public class ProductoQuerysController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ResponseHttp _responseHttp;

    public ProductoQuerysController(IMediator mediator, ResponseHttp responseHttp)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _responseHttp = responseHttp ?? throw new ArgumentNullException(nameof(responseHttp));

    }

    [HttpGet("")]
    [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllProductoQuery());
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpGet("ById{id}")]
    [ProducesResponseType(typeof(Responses<List<ProductoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetByIdProductoQuery(id));
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpGet("ByName{nombre}")]
    [ProducesResponseType(typeof(Responses<List<ProductoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByNameLike(string nombre)
    {
        var response = await _mediator.Send(new GetByNameProductoQuery(nombre));
        return await _responseHttp.GetResponseHttp(response);
    }
}
