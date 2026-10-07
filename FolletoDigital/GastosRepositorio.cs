namespace FolletoDigital;

public static class GastosRepositorio
{
    private static readonly List<Gasto> Gastos = new();

    public static IReadOnlyList<Gasto> ObtenerTodos()
    {
        return Gastos.AsReadOnly();
    }

    public static void Agregar(Gasto gasto)
    {
        ArgumentNullException.ThrowIfNull(gasto);
        Gastos.Add(gasto);
    }

    public static bool Eliminar(int indice)
    {
        if (indice < 0 || indice >= Gastos.Count)
        {
            return false;
        }

        Gastos.RemoveAt(indice);
        return true;
    }

    public static decimal ObtenerTotal()
    {
        return Gastos.Sum(gasto => gasto.Monto);
    }

    public static int ObtenerCantidad()
    {
        return Gastos.Count;
    }

    public static Gasto? ObtenerMayorMonto()
    {
        return Gastos
            .OrderByDescending(gasto => gasto.Monto)
            .FirstOrDefault();
    }

    public static string ObtenerCategoriaPredominante()
    {
        if (Gastos.Count == 0)
        {
            return "Ninguna";
        }

        return Gastos
            .GroupBy(gasto => gasto.Categoria)
            .OrderByDescending(grupo => grupo.Sum(gasto => gasto.Monto))
            .First()
            .Key;
    }

    public static Dictionary<string, decimal> ObtenerTotalesPorCategoria()
    {
        return Gastos
            .GroupBy(gasto => gasto.Categoria)
            .ToDictionary(
                grupo => grupo.Key,
                grupo => grupo.Sum(gasto => gasto.Monto));
    }
}
