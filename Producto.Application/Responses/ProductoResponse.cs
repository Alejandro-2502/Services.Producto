namespace Producto.Application.Responses;

public class ProductoResponse
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaAlta { get; set; }
}
