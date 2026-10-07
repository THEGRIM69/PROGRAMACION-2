using SystemAnalyzer.UI;

namespace SystemAnalyzer.Forms;

public sealed class FormCreditos : BaseContentForm
{
    public FormCreditos() : base("Acerca de / Créditos", "Información académica del proyecto")
    {
        ContentPanel.Controls.Add(new Label
        {
            Dock = DockStyle.Fill,
            Text = "SystemAnalyzer\nAnalizador de Sistema Operativo y Rendimiento\n\nDesarrollado por:\nIng. Rodrigo Flores\nCarnet: F1631012025\n\nProyecto desarrollado en C# — Windows Forms",
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = AppTheme.Text,
            Font = new Font("Segoe UI", 13F),
            Padding = new Padding(30)
        });
    }
}
