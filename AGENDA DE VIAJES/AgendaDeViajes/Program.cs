namespace AgendaDeViajes
{
    /// <summary>
    /// Punto de entrada de la aplicación Agenda de Vuelos.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.FrmPrincipal());
        }
    }
}
