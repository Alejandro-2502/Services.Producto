using FluentValidation;
using Producto.Application.Helpers;
using Producto.Application.Messages;
using Producto.Application.Requests;

namespace Producto.Application.UsersHistorys.Commands.UpdateProducto
{
    public class UpdateProductoValidator : AbstractValidator<ProductoRequest>
    {
        public UpdateProductoValidator()
        {
            RuleFor(prod => prod.Nombre).NotNull().NotEmpty().WithMessage(MensajesValidationsProducto.ValidationsProductoNonbreNotNull)
               .MaximumLength(15).WithMessage(MensajesValidationsProducto.ValidationsProductoNombreMax20Caracteres)
               .Must(CadenasHelper.ExisteCaracteresEspeciales).WithMessage(MensajesValidationsProducto.ValidationsProductoCaracteresEspeciales);
            RuleFor(prod => prod.Stock).NotNull().NotEmpty().WithMessage(MensajesValidationsProducto.ValidationsProductoStockMayorACero)
                .GreaterThan(0).WithMessage(MensajesValidationsProducto.ValidationsProductoStockMayorACero);
            RuleFor(prod => prod.Precio).NotNull().NotEmpty().WithMessage(MensajesValidationsProducto.ValidationsProductoPrecioNoNull)
                .GreaterThan(0).WithMessage(MensajesValidationsProducto.ValidationsProductoPrecioMayorACero);
        }
    }
}
