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
    public partial class Micro : Form
    {
        private PacienteM pacienteActivo;
        public Micro()
        {
            InitializeComponent();
        }
        private int idPaciente;
        public Micro(int id)
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

                // Cargar examen si existe
                MicroM examen = MicroLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    txtGram.Text = examen.Muestra;
                    txtMuestra.Text = examen.Gram;
                    txtM1.Text = examen.M1;
                    txtM2.Text = examen.M2;
                    txtCultivo.Text = examen.M3;
                    txtM3.Text = examen.Cultivo;
                    txtColonia.Text = examen.Colonia;
                    txtIdentificacion.Text = examen.Identificacion;
                    txtSensibles.Text = examen.Sensible;
                    txtResistentes.Text = examen.Resistencia;
                    txtNota.Text = examen.Nota;
                }
            }
            else
            {
                MessageBox.Show("Paciente no encontrado.");
            }
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            if (pacienteActivo == null)
            {
                MessageBox.Show("No hay un paciente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MicroM examen = new MicroM()
            {
                Muestra = txtGram.Text,
                Gram = txtMuestra.Text,
                M1 = txtM1.Text,
                M2 = txtM2.Text,
                M3 = txtCultivo.Text,
                Cultivo = txtM3.Text,
                Colonia = txtColonia.Text,
                Identificacion = txtIdentificacion.Text,
                Sensible = txtSensibles.Text,
                Resistencia = txtResistentes.Text,
                Nota = txtNota.Text,
            };

            bool existe = MicroLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;

            bool resultado = existe ?
                MicroLogica.Instancia.ActualizarExamen(examen, idPaciente) :
                MicroLogica.Instancia.GuardarExamen(examen, idPaciente);

            if (resultado)
            {
                MessageBox.Show(existe ? "Examen actualizado" : "Examen guardado", "Éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar o actualizar", "Error");
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

        private void Micro_Load(object sender, EventArgs e)
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


        private void btnNuevopaciente_Click_1(object sender, EventArgs e)
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
                Serologia form = new Serologia(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Hemograma form = new Hemograma(pacienteActivo.IdPaciente);
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtGram.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }
    }
}
