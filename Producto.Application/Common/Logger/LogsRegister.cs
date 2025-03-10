using static Producto.Application.Enums.LoggerTypes;

namespace Producto.Application.Common.Logger;

public class LogsRegister
{
    public LoggerType Type { get; set; }
    public string? Messages { get; set; }
}
