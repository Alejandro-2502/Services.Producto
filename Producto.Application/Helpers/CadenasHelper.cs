using Producto.Application.Configurations;

namespace Producto.Application.Helpers;

public static class CadenasHelper
{
    public static bool ExisteCaracteresEspeciales(string? valor)
    {
        bool result = !ConfigHelper.ConfigFormatos!.CaracteresEspeciales!.Intersect(valor).Any();
        return result;
    }
}
