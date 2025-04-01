using Laboratorio.Vista;
using prueba.Logica;
using prueba.Logica_Sevicio;
using prueba.Modelo;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba.Vista
{
    public partial class Serologia : Form
    {
        private PacienteM pacienteActivo;
        private int idPaciente;
        public Serologia(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
        }
        private void CargarDatos()
        {
            pacienteActivo = PacienteLogica.Instancia.BuscarPorId(idPaciente);
            if (pacienteActivo != null)
            {
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;
                panel1.Visible = true;

                SerologiaM examen = SerologiaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    txtReaccion.Text = examen.Reaccion;
                    txtCampo11.Text = examen.Campo11;
                    c12.Text = examen.Campo12;
                    c13.Text = examen.Campo13;
                    c14.Text = examen.Campo14;
                    c15.Text = examen.Campo15;
                    c21.Text = examen.Campo21;
                    c22.Text = examen.Campo22;
                    c23.Text = examen.Campo23;
                    c24.Text = examen.Campo24;
                    c25.Text = examen.Campo25;
                    c31.Text = examen.Campo31;
                    c32.Text = examen.Campo32;
                    c33.Text = examen.Campo33;
                    c34.Text = examen.Campo34;
                    c35.Text = examen.Campo35;
                    c41.Text = examen.Campo41;
                    c42.Text = examen.Campo42;
                    c43.Text = examen.Campo43;
                    c44.Text = examen.Campo44;
                    c45.Text = examen.Campo45;
                    txtObservaciones.Text = examen.Observaciones;
                }
            }
        }

        public Serologia()
        {
            InitializeComponent();
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            if (pacienteActivo == null)
            {
                MessageBox.Show("No hay un paciente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SerologiaM objeto = new SerologiaM()
                {
                    Reaccion = txtReaccion.Text,
                    Campo11 = txtCampo11.Text,
                    Campo12 = c12.Text,
                    Campo13 = c13.Text,
                    Campo14 = c14.Text,
                    Campo15 = c15.Text,
                    Campo21 = c21.Text,
                    Campo22 = c22.Text,
                    Campo23 = c23.Text,
                    Campo24 = c24.Text,
                    Campo25 = c25.Text,
                    Campo31 = c31.Text,
                    Campo32 = c32.Text,
                    Campo33 = c33.Text,
                    Campo34 = c34.Text,
                    Campo35 = c35.Text,
                    Campo41 = c41.Text,
                    Campo42 = c42.Text,
                    Campo43 = c43.Text,
                    Campo44 = c44.Text,
                    Campo45 = c45.Text,
                    Observaciones = txtObservaciones.Text
                };

                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores si pacienteActivo es null

                // Mostrar los datos antes de guardar
                bool existe = SerologiaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;

                bool resultado = existe ?
                    SerologiaLogica.Instancia.ActualizarExamen(objeto, idPaciente) :
                    SerologiaLogica.Instancia.GuardarExamen(objeto, idPaciente);

                if (resultado)
                {
                    MessageBox.Show(existe ? "Examen actualizado correctamente." : "Examen guardado correctamente.", "Éxito");
                }
                else
                {
                    MessageBox.Show("Error al guardar o actualizar el examen de Serología.", "Error");
                }

                CapturarPanel(PanelCap); // Reemplaza 'panel1' con el nombre de tu panel.

            // Configurar el documento de impresión
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Mostrar el cuadro de diálogo de impresión
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
         

        }


        private Bitmap panelBitmap;
        private void CapturarPanel(Panel panel)
        {
            // Ocultar solo los botones de paneles superpuestos antes de capturar
            OcultarBotonesEnPanelesSuperpuestos(this);

            // Crear un Bitmap con el tamaño del panel principal
            panelBitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panel.Width, panel.Height));

            // Restaurar visibilidad de los botones después de capturar
            MostrarBotonesEnPanelesSuperpuestos(this);
        }

        // 🔹 Método para ocultar botones en paneles superpuestos
        private void OcultarBotonesEnPanelesSuperpuestos(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Panel && control != PanelCap) // Excluir el PanelCap de la captura
                {
                    OcultarBotones(control);
                }

                // Si el control tiene controles anidados, buscar recursivamente
                if (control.HasChildren)
                {
                    OcultarBotonesEnPanelesSuperpuestos(control);
                }
            }
        }

        // 🔹 Método para restaurar visibilidad de botones después de capturar
        private void MostrarBotonesEnPanelesSuperpuestos(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Panel && control != PanelCap)
                {
                    MostrarBotones(control);
                }

                if (control.HasChildren)
                {
                    MostrarBotonesEnPanelesSuperpuestos(control);
                }
            }
        }

        // ✅ Ocultar botones específicos dentro de un panel
        private void OcultarBotones(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button)
                {
                    control.Visible = false; // Ocultar botones
                }
            }
        }

        // ✅ Restaurar visibilidad de los botones
        private void MostrarBotones(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button)
                {
                    control.Visible = true; // Mostrar botones nuevamente
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Dibujar el contenido del panel capturado en la página de impresión
            if (panelBitmap != null)
            {
                e.Graphics.DrawImage(panelBitmap, new Point(0, 0));
            }
        }
        private void Serologia_Load(object sender, EventArgs e)
        {

            dtpFecha.Value = DateTime.Now;
            // Obtener el último paciente registrado
            //PacienteM paciente = PacienteLogica.Instancia.ObtenerUltimoPaciente();
            if (pacienteActivo != null) // Verificar si se encontró un paciente
            {
                // Mostrar el panel y labels
                panel1.Visible = true;
                lblNombreCompleto.Visible = true;
                lblEdad.Visible = true;
                lblMedico.Visible = true;

                // Asignar los valores obtenidos
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}  {pacienteActivo.ApellidoM}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;
            }
            else
            {
                MessageBox.Show("No hay un paciente activo. Registre un paciente antes de continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNuevoPAciente_Click_1(object sender, EventArgs e)
        {
            RegistroPaciente formQuimica = new RegistroPaciente();
            formQuimica.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reporte formQuimica = new Reporte();
            formQuimica.Show();
            this.Hide();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblNombreCompleto_Click(object sender, EventArgs e)
        {

        }

        private void lblMedico_Click(object sender, EventArgs e)
        {

        }

        private void label65_Click(object sender, EventArgs e)
        {

        }

    

        private void btnHemograma_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Quimica form = new Quimica(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnOrina_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Orina form = new Orina(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnCopros_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Copros form = new Copros(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnHCG_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                HCG form = new HCG(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnSerologia_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Hemograma form = new Hemograma(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Micro form = new Micro(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnBlanco_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Blanco form = new Blanco(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Sobre form = new Sobre(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnVarios_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Varios form = new Varios(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }
        private void MostrarAdvertencia()
        {
            MessageBox.Show("No hay un paciente activo. Registre o seleccione uno antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtReaccion.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }
    }
}
