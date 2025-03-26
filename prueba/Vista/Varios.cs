using Laboratorio.Vista;
using prueba.Logica;
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
using static System.Net.Mime.MediaTypeNames;

namespace prueba.Vista
{
    public partial class Varios : Form
    {
        private PacienteM pacienteActivo;

        public Varios()
        {
            InitializeComponent();
        }
        private int idPaciente;
        public Varios(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
        }
        private void CargarDatos()
        {
           
        }
        private void Varios_Load(object sender, EventArgs e)
        {

            dtpFecha.Value = DateTime.Now;
            // Obtener el último paciente registrado
            //PacienteM paciente = PacienteLogica.Instancia.ObtenerUltimoPaciente();
            pacienteActivo = PacienteLogica.Instancia.ObtenerUltimoPaciente();
            if (pacienteActivo != null) // Verificar si se encontró un paciente
            {
                // Mostrar el panel y labels
                panel1.Visible = true;
                lblNombreCompleto.Visible = true;
                lblEdad.Visible = true;
                lblMedico.Visible = true;

                // Asignar los valores obtenidos
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;
            }
            else
            {
                MessageBox.Show("No hay un paciente activo. Registre un paciente antes de continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
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
            // Crear un Bitmap con el tamaño del panel
            panelBitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panel.Width, panel.Height));
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Dibujar el contenido del panel capturado en la página de impresión
            if (panelBitmap != null)
            {
                e.Graphics.DrawImage(panelBitmap, new Point(0, 0));
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

        private void lblNombreCompleto_Click(object sender, EventArgs e)
        {

        }

        private void label65_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
