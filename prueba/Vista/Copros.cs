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
    public partial class Copros : Form
    {
        private PacienteM pacienteActivo;
        public Copros()
        {

            InitializeComponent();
            LlenarDataGridView();
        }
        private int idPaciente;
        public Copros(int id)
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
                panel1.Visible = true;
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;

                CoprosM examen = CoprosLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    txtConsistencia.Text = examen.Consistencia;
                    txtColor.Text = examen.Color;
                    txtExamenM.Text = examen.ExamenM;
                    txtObservaciones.Text = examen.Observaciones;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron datos del paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Copros_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
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
        private void LlenarDataGridView()
        {
            dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
        }
        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            if (pacienteActivo == null)
            {
                MessageBox.Show("No hay un paciente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CoprosM objeto = new CoprosM()
            {
                Consistencia = txtConsistencia.Text,
                Color = txtColor.Text,
                ExamenM = txtExamenM.Text,
                Observaciones = txtObservaciones.Text
            };

            int id = pacienteActivo.IdPaciente;

            bool existe = CoprosLogica.Instancia.ObtenerExamenPorPaciente(id) != null;
            bool resultado = existe
                ? CoprosLogica.Instancia.ActualizarExamen(objeto, id)
                : CoprosLogica.Instancia.GuardarExamen(objeto, id);

            if (resultado)
            {
                MessageBox.Show("Examen de Copros guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapturarPanel(PanelCap);

                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintDocument_PrintPage;
                printDialog.Document = printDocument;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar o actualizar el examen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnHemograma_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Quimica form = new Quimica(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnOrina_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Orina form = new Orina(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnCopros_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Hemograma form = new Hemograma(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnHCG_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                HCG form = new HCG(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnSerologia_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Serologia form = new Serologia(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Micro form = new Micro(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnBlanco_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Blanco form = new Blanco(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Sobre form = new Sobre(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnVarios_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Varios form = new Varios(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }
        private void MostrarAdvertencia()
        {
            MessageBox.Show("No hay un paciente activo. Registre uno antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void btnNuevoPaciente_Click_1(object sender, EventArgs e)
        {
            RegistroPaciente formQuimica = new RegistroPaciente();
            formQuimica.Show();
            this.Hide();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Reporte formQuimica = new Reporte();
            formQuimica.Show();
            this.Hide();
        }
    }
}

