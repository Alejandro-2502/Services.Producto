namespace Producto.Application.Requests;

public class ProductoRequest
{
    public int id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}
