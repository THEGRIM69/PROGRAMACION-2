using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using AgendaDeViajes.Data;
using AgendaDeViajes.Services;

namespace AgendaDeViajes.Forms
{
    /// <summary>
    /// Formulario principal de la aplicación. Actúa como contenedor de los módulos
    /// del sistema mediante un panel central, un árbol de navegación y un menú superior.
    /// </summary>
    public partial class FrmPrincipal : Form
    {
        private const string ModuloDashboard = "dashboard";
        private const string ModuloVuelos = "vuelos";
        private const string ModuloPasajeros = "pasajeros";
        private const string ModuloReservaciones = "reservaciones";
        private const string ModuloReportes = "reportes";
        private const string ModuloAcercaDe = "acerca";

        private static readonly Color ColorAcento = Color.FromArgb(45, 156, 219);
        private static readonly Color ColorTitulo = Color.FromArgb(30, 42, 56);
        private static readonly Color ColorTextoSecundario = Color.FromArgb(107, 122, 140);

        private static readonly CultureInfo Cultura = ObtenerCulturaEspanola();

        private readonly AgendaRepository repositorio = new AgendaRepository();

        private AgendaData datos;

        private FrmDashboard dashboard;

        private FrmAcercaDe acercaDe;

        private FrmGestionVuelos gestionVuelos;

        private FrmGestionPasajeros gestionPasajeros;

        private FrmGestionReservaciones gestionReservaciones;

        private FrmReportes reportes;

        public FrmPrincipal()
        {
            InitializeComponent();
            InicializarAgenda();
            ConstruirTarjetasModulos();
            ActualizarReloj();
            timerReloj.Start();

            if (treeViewNavegacion.Nodes.Count > 0)
            {
                treeViewNavegacion.SelectedNode = treeViewNavegacion.Nodes[0];
            }
        }

        /// <summary>
        /// Datos de la agenda cargados desde el archivo JSON local.
        /// </summary>
        public AgendaData Datos
        {
            get { return datos; }
        }

        /// <summary>
        /// Ruta del archivo JSON utilizado por la aplicación.
        /// </summary>
        public string RutaArchivoDatos
        {
            get { return repositorio.RutaArchivo; }
        }

        /// <summary>
        /// Guarda los cambios en disco mediante persistencia atómica en JSON,
        /// actualiza los contadores de la barra de estado y refresca el Dashboard.
        /// </summary>
        public bool GuardarCambios()
        {
            try
            {
                repositorio.Guardar(datos);
                ActualizarContadores();
                RefrescarDashboard();
                lblEstado.Text = "Cambios guardados · " + DateTime.Now.ToString("HH:mm:ss");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Error al guardar los datos de la agenda:" + Environment.NewLine + Environment.NewLine + ex.Message,
                    "Error de persistencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        #region Carga de datos

        private void InicializarAgenda()
        {
            try
            {
                ResultadoCarga resultado = repositorio.Cargar();

                if (resultado.ArchivoNuevo)
                {
                    AgendaData nuevosDatos = DatosDemostracion.Crear();
                    ResultadoValidacion validacion = ValidadorAgenda.Validar(nuevosDatos);

                    if (!validacion.EsValido)
                    {
                        MessageBox.Show(
                            this,
                            "Los datos de demostración no cumplen las reglas de integridad y no se guardaron:"
                            + Environment.NewLine + Environment.NewLine + validacion,
                            "Agenda de Vuelos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        lblEstado.Text = "Datos de demostración inválidos";
                        lblRegistros.Text = "Sin datos cargados";
                        return;
                    }

                    repositorio.Guardar(nuevosDatos);
                    datos = nuevosDatos;
                    lblEstado.Text = "Datos de demostración creados";
                }
                else if (!resultado.Exito)
                {
                    datos = new AgendaData();
                    lblEstado.Text = resultado.ArchivoDanado
                        ? "Archivo de datos dañado"
                        : "No se pudo cargar la agenda";
                    lblRegistros.Text = "Sin datos cargados";

                    MessageBox.Show(
                        this,
                        resultado.Mensaje
                        + Environment.NewLine + Environment.NewLine
                        + "Archivo: " + repositorio.RutaArchivo,
                        "Agenda de Vuelos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
                else
                {
                    datos = resultado.Datos;
                    lblEstado.Text = "Agenda cargada";
                }

                ActualizarContadores();
            }
            catch (Exception ex)
            {
                datos = new AgendaData();
                lblEstado.Text = "Error al inicializar la agenda";
                lblRegistros.Text = "Sin datos cargados";

                MessageBox.Show(
                    this,
                    "No fue posible inicializar la agenda." + Environment.NewLine + Environment.NewLine + ex.Message,
                    "Agenda de Vuelos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ActualizarContadores()
        {
            if (datos == null)
            {
                lblRegistros.Text = "Sin datos cargados";
                return;
            }

            lblRegistros.Text = datos.Vuelos.Count + " vuelos · "
                + datos.Pasajeros.Count + " pasajeros · "
                + datos.Reservaciones.Count + " reservaciones";
        }

        #endregion

        #region Navegación

        private void treeViewNavegacion_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is string clave)
            {
                MostrarModulo(clave);
            }
        }

        private void MostrarModulo(string clave)
        {
            switch (clave)
            {
                case ModuloDashboard:
                    MostrarDashboard();
                    break;

                case ModuloVuelos:
                    MostrarVuelos();
                    break;

                case ModuloPasajeros:
                    MostrarPasajeros();
                    break;

                case ModuloReservaciones:
                    MostrarReservaciones();
                    break;

                case ModuloReportes:
                    MostrarReportes();
                    break;

                case ModuloAcercaDe:
                    MostrarAcercaDe();
                    break;
            }
        }

        private void OcultarVistas()
        {
            if (dashboard != null) dashboard.Visible = false;
            if (gestionVuelos != null) gestionVuelos.Visible = false;
            if (gestionPasajeros != null) gestionPasajeros.Visible = false;
            if (gestionReservaciones != null) gestionReservaciones.Visible = false;
            if (reportes != null) reportes.Visible = false;
            if (acercaDe != null) acercaDe.Visible = false;

            panelBienvenida.Visible = false;
            panelMarcador.Visible = false;
        }

        private void MostrarMarcador(string titulo, string descripcion, string fase, string estado)
        {
            OcultarVistas();
            lblModuloTitulo.Text = titulo;
            lblModuloDescripcion.Text = descripcion;
            lblMarcadorTitulo.Text = titulo;
            lblMarcadorTexto.Text = descripcion;
            lblMarcadorFase.Text = fase;
            panelMarcador.Visible = true;
            lblEstado.Text = estado;
        }

        /// <summary>
        /// Muestra el panel principal dentro del contenedor central, creándolo la
        /// primera vez que se accede al módulo.
        /// </summary>
        private void MostrarDashboard()
        {
            OcultarVistas();

            if (dashboard == null)
            {
                dashboard = new FrmDashboard
                {
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false
                };

                panelContenido.Controls.Add(dashboard);
            }

            dashboard.ActualizarDatos(datos);
            dashboard.Visible = true;
            dashboard.BringToFront();

            lblModuloTitulo.Text = "Panel principal";
            lblModuloDescripcion.Text = "Resumen general de la operación de la agencia";
            lblEstado.Text = "Panel principal";
        }

        /// <summary>
        /// Muestra la vista «Acerca de» dentro del contenedor central, creándola
        /// la primera vez que se accede al módulo.
        /// </summary>
        private void MostrarAcercaDe()
        {
            OcultarVistas();

            if (acercaDe == null)
            {
                acercaDe = new FrmAcercaDe
                {
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false
                };

                panelContenido.Controls.Add(acercaDe);
            }

            acercaDe.Visible = true;
            acercaDe.BringToFront();

            lblModuloTitulo.Text = "Acerca de";
            lblModuloDescripcion.Text = "Información del sistema, tecnologías y créditos";
            lblEstado.Text = "Acerca de";
        }

        /// <summary>
        /// Muestra el módulo de gestión de vuelos dentro del contenedor central,
        /// creándolo la primera vez que se accede al módulo.
        /// </summary>
        private void MostrarVuelos()
        {
            OcultarVistas();

            if (gestionVuelos == null)
            {
                gestionVuelos = new FrmGestionVuelos(this)
                {
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false
                };

                panelContenido.Controls.Add(gestionVuelos);
            }

            gestionVuelos.Visible = true;
            gestionVuelos.BringToFront();
            gestionVuelos.CargarVuelos();

            lblModuloTitulo.Text = "Gestión de vuelos";
            lblModuloDescripcion.Text = "Consulta, registro, edición y control de vuelos de la agencia";
            lblEstado.Text = "Módulo de vuelos";
        }

        /// <summary>
        /// Muestra el módulo de gestión de pasajeros dentro del contenedor central,
        /// creándolo la primera vez que se accede al módulo.
        /// </summary>
        private void MostrarPasajeros()
        {
            OcultarVistas();

            if (gestionPasajeros == null)
            {
                gestionPasajeros = new FrmGestionPasajeros(this)
                {
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false
                };

                panelContenido.Controls.Add(gestionPasajeros);
            }

            gestionPasajeros.Visible = true;
            gestionPasajeros.BringToFront();
            gestionPasajeros.CargarPasajeros();

            lblModuloTitulo.Text = "Gestión de pasajeros";
            lblModuloDescripcion.Text = "Consulta, registro, edición y preferencias de los pasajeros de la agencia";
            lblEstado.Text = "Módulo de pasajeros";
        }

        /// <summary>
        /// Muestra el módulo de gestión de reservaciones dentro del contenedor central,
        /// creándolo la primera vez que se accede al módulo.
        /// </summary>
        private void MostrarReservaciones()
        {
            OcultarVistas();

            if (gestionReservaciones == null)
            {
                gestionReservaciones = new FrmGestionReservaciones(this)
                {
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false
                };

                panelContenido.Controls.Add(gestionReservaciones);
            }

            gestionReservaciones.Visible = true;
            gestionReservaciones.BringToFront();
            gestionReservaciones.CargarReservaciones();

            lblModuloTitulo.Text = "Gestión de reservaciones";
            lblModuloDescripcion.Text = "Asignación de asientos, control de cupos, tarifas y servicios adicionales";
            lblEstado.Text = "Módulo de reservaciones";
        }

        /// <summary>
        /// Muestra el módulo de consultas y reportes dentro del contenedor central,
        /// creándolo la primera vez que se accede al módulo.
        /// </summary>
        private void MostrarReportes()
        {
            OcultarVistas();

            if (reportes == null)
            {
                reportes = new FrmReportes(this)
                {
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false
                };

                panelContenido.Controls.Add(reportes);
            }

            reportes.Visible = true;
            reportes.BringToFront();
            reportes.GenerarReporte();

            lblModuloTitulo.Text = "Consultas y reportes";
            lblModuloDescripcion.Text = "Métricas consolidadas, balances analíticos y reportes de la agenda";
            lblEstado.Text = "Módulo de reportes";
        }

        /// <summary>
        /// Vuelve a calcular el Dashboard con los datos vigentes de la agenda.
        /// </summary>
        public void RefrescarDashboard()
        {
            if (dashboard != null && dashboard.Visible)
            {
                dashboard.ActualizarDatos(datos);
            }

            if (reportes != null && reportes.Visible)
            {
                reportes.GenerarReporte();
            }
        }

        private void SeleccionarNodo(string clave)
        {
            foreach (TreeNode nodo in treeViewNavegacion.Nodes)
            {
                if (nodo.Tag is string valor && valor == clave)
                {
                    treeViewNavegacion.SelectedNode = nodo;
                    treeViewNavegacion.Focus();
                    return;
                }
            }
        }

        #endregion

        #region Menú

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuVerDashboard_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloDashboard);
        }

        private void mnuVerVuelos_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloVuelos);
        }

        private void mnuVerPasajeros_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloPasajeros);
        }

        private void mnuVerReservaciones_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloReservaciones);
        }

        private void mnuVerReportes_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloReportes);
        }

        private void mnuNuevoVuelo_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloVuelos);
            gestionVuelos?.NuevoVuelo();
        }

        private void mnuNuevoPasajero_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloPasajeros);
            gestionPasajeros?.NuevoPasajero();
        }

        private void mnuNuevaReservacion_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloReservaciones);
            gestionReservaciones?.NuevaReservacion();
        }

        private void mnuImportar_Click(object sender, EventArgs e)
        {
            MostrarAvisoFase("Importación de la agenda", "Fase 8");
        }

        private void mnuExportar_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloReportes);
        }

        private void mnuColorAcento_Click(object sender, EventArgs e)
        {
            MostrarAvisoFase("Personalización del tema", "Fase 8");
        }

        private void mnuAcercaDe_Click(object sender, EventArgs e)
        {
            SeleccionarNodo(ModuloAcercaDe);
        }

        private void MostrarAvisoFase(string modulo, string fase)
        {
            MessageBox.Show(
                this,
                "El módulo de " + modulo.ToLowerInvariant() + " estará disponible en la " + fase + ".",
                "Agenda de Vuelos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            lblEstado.Text = modulo + " · pendiente (" + fase + ")";
        }

        #endregion

        #region Tarjetas del panel principal

        private void ConstruirTarjetasModulos()
        {
            flowModulos.Controls.Clear();

            AgregarTarjeta(
                "Vuelos",
                "Registro, consulta y edición de los vuelos programados por la agencia.",
                "Módulo disponible · Fase 4");

            AgregarTarjeta(
                "Pasajeros",
                "Administración de datos personales, documentos y preferencias.",
                "Módulo disponible · Fase 6 (CRUD funcional)");

            AgregarTarjeta(
                "Reservaciones",
                "Asignación de asientos, tarifas y servicios sobre cada vuelo.",
                "Módulo disponible · Fase 7 (CRUD funcional)");

            AgregarTarjeta(
                "Reportes",
                "Métricas analíticas, balances por aerolínea, exportación CSV, PDF e impresión.",
                "Módulo disponible · Fase 8 (Reportes, exportación CSV/PDF e impresión)");
        }

        private void AgregarTarjeta(string titulo, string descripcion, string nota)
        {
            var tarjeta = new Panel
            {
                BackColor = Color.White,
                Margin = new Padding(0, 0, 16, 16),
                Size = new Size(266, 124)
            };

            var franja = new Panel
            {
                BackColor = ColorAcento,
                Dock = DockStyle.Left,
                Width = 4
            };

            var lblTituloTarjeta = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = ColorTitulo,
                Location = new Point(18, 14),
                Size = new Size(228, 24),
                Text = titulo
            };

            var lblDescripcionTarjeta = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9F),
                ForeColor = ColorTextoSecundario,
                Location = new Point(18, 40),
                Size = new Size(228, 50),
                Text = descripcion
            };

            var lblNotaTarjeta = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI Semibold", 8F),
                ForeColor = ColorAcento,
                Location = new Point(18, 94),
                Size = new Size(228, 18),
                Text = nota
            };

            tarjeta.Controls.Add(franja);
            tarjeta.Controls.Add(lblTituloTarjeta);
            tarjeta.Controls.Add(lblDescripcionTarjeta);
            tarjeta.Controls.Add(lblNotaTarjeta);
            flowModulos.Controls.Add(tarjeta);
        }

        #endregion

        #region Reloj de la barra de estado

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            ActualizarReloj();
        }

        private void ActualizarReloj()
        {
            lblFecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy · HH:mm", Cultura);
        }

        private static CultureInfo ObtenerCulturaEspanola()
        {
            try
            {
                return CultureInfo.GetCultureInfo("es-SV");
            }
            catch (CultureNotFoundException)
            {
                return CultureInfo.CurrentCulture;
            }
        }

        #endregion

        /// <summary>
        /// Tabla de colores del tema visual usada por el menú y la barra de estado.
        /// </summary>
        private sealed class TemaMenu : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Color.White;

            public override Color MenuStripGradientEnd => Color.White;

            public override Color MenuItemSelected => Color.FromArgb(234, 243, 251);

            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(234, 243, 251);

            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(234, 243, 251);

            public override Color MenuItemPressedGradientBegin => Color.FromArgb(220, 235, 248);

            public override Color MenuItemPressedGradientEnd => Color.FromArgb(220, 235, 248);

            public override Color MenuItemBorder => Color.FromArgb(45, 156, 219);

            public override Color MenuBorder => Color.FromArgb(224, 229, 235);

            public override Color ToolStripDropDownBackground => Color.White;

            public override Color ImageMarginGradientBegin => Color.White;

            public override Color ImageMarginGradientMiddle => Color.White;

            public override Color ImageMarginGradientEnd => Color.White;

            public override Color SeparatorDark => Color.FromArgb(225, 230, 236);

            public override Color SeparatorLight => Color.White;

            public override Color StatusStripGradientBegin => Color.FromArgb(22, 34, 47);

            public override Color StatusStripGradientEnd => Color.FromArgb(22, 34, 47);
        }
    }
}
