namespace FolletoDigital;

public class Gasto
{
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Monto { get; set; }
}
