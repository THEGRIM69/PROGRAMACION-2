namespace AgendaDeViajes.Models
{
    /// <summary>
    /// Situación operativa en la que se encuentra un vuelo.
    /// </summary>
    public enum EstadoVuelo
    {
        Programado,
        Abordando,
        Retrasado,
        EnVuelo,
        Finalizado,
        Cancelado
    }
}
