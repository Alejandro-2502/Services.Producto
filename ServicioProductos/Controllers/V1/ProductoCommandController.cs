using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producto.Application.Generics;
using Producto.Application.Requests;
using Producto.Application.Responses;
using static Producto.Application.UsersHistorys.Commands.CreateProducto.CreateProductoHandler;
using static Producto.Application.UsersHistorys.Commands.DeleteProducto.DeleteProductoHandler;
using static Producto.Application.UsersHistorys.Commands.UpdateProducto.UpdateProductoHandler;

namespace Producto.Services.Controllers.V1
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoCommandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ResponseHttp _responseHttp;

        public ProductoCommandController(IMediator mediator, ResponseHttp responseHttp)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _responseHttp = responseHttp ?? throw new ArgumentNullException(nameof(responseHttp));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ProductoRequest productoRequest)
        {
            if (productoRequest == null)
                return BadRequest();

            var response = await _mediator.Send(new CreateProductoCommand(productoRequest));
            return await _responseHttp.GetResponseHttp(response);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] ProductoRequest productoRequest)
        {
            if (productoRequest == null)
                return BadRequest();

            var response = await _mediator.Send(new UpdateProductoCommand(productoRequest));
            return await _responseHttp.GetResponseHttp(response);
        }

        [HttpDelete()]
        [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ProductoResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] ProductoRequest productoRequest)
        {
            if (productoRequest == null)
                return BadRequest();

            var response = await _mediator.Send(new DeleteProductoCommand(productoRequest));
            return await _responseHttp.GetResponseHttp(response); ;
        }
    }
}
