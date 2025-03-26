using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laboratorio.Vista;
using prueba.Logica;
using prueba.Logica_Servicio;
using Prueba.Modelo;

namespace prueba.Vista
{
    public partial class Orina : Form
    {

        private PacienteM pacienteActivo;

        public Orina()
        {
            InitializeComponent();
            //   dgvOrina.CellClick += new DataGridViewCellEventHandler(dgvOrina_CellContentClick);
            
            LlenarDataGridView();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private int idPaciente;

        public Orina(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
        }
        private void CargarDatos()
        {
            // Aquí consultas la base de datos y llenas los campos con los datos del paciente
        }
        private void Orina_Load(object sender, EventArgs e)
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

        private void LlenarDataGridView()
        {
            dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
        }
     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {

            if (pacienteActivo == null) // Verificar si hay un paciente cargado
            {
                MessageBox.Show("No hay un paciente activo. Registre un paciente antes de continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrinaM examenOrina = new OrinaM()
            {
                Aspecto = txtAspecto.Text,
                Color = txtColor.Text,
                Olor = txtOlor.Text,
                Densidad = txtDensidad.Text,
                Reaccion = txtReaccion.Text,
                Glucosa = txtGlucosa.Text,
                Bilirrubina = txtBilirrubina.Text,
                Cetonas = txtCetonas.Text,
                Sangre = txtSangre.Text,
                Proteina = txtProteina.Text,
                Urobiliogeno = txtUrobiliogeno.Text,
                Nitrito = txtNitrito.Text,
                Leucocito1 = txtLeucocitos.Text,
                Eritrocito = txtEritrocitos.Text,
                Leucocito2 = txtLeucocitos1.Text,
                CED = txtCED.Text,
                Redonda = txtRedondas.Text,
                Embarazo = txtEmbarazo.Text,
                Otros = txtOtros.Text,
                Observaciones = txtObservaciones.Text,
                Flora = txtFlora.Text,
                Piocito = txtPiocitos.Text,
                Cristale = txtCristales.Text,
                Cilindro = txtCilindros.Text
            };

            int idPaciente = pacienteActivo.IdPaciente; // Usamos la variable global

            bool guardado = OrinaLogica.Instancia.GuardarExamen(examenOrina, idPaciente);

            if (guardado)
            {
                MessageBox.Show("Examen de orina guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Console.WriteLine();

                MessageBox.Show("Error al guardar el examen de orina. Verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnBlanco_Click(object sender, EventArgs e)
        {
            Blanco formQuimica = new Blanco();
            formQuimica.Show();
            this.Hide();
        }

        private void btnHemograma_Click(object sender, EventArgs e)
        {
            Quimica formQuimica = new Quimica();
            formQuimica.Show();  // Abre el formulario de Química
            this.Hide();  // Oculta el formulario actual
        }

        private void btnOrina_Click(object sender, EventArgs e)
        {
            Hemograma formHemograma = new Hemograma(PacienteActivo.IdPaciente);
            formHemograma.Show();

        }

        private void btnCopros_Click(object sender, EventArgs e)
        {
            Copros formQuimica = new Copros();
            formQuimica.Show();
            this.Hide();
        }

        private void btnHCG_Click(object sender, EventArgs e)
        {
            HCG formQuimica = new HCG();
            formQuimica.Show();
            this.Hide();
        }

        private void btnSerologia_Click(object sender, EventArgs e)
        {
            Serologia formQuimica = new Serologia();
            formQuimica.Show();
            this.Hide();
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            Micro formQuimica = new Micro();
            formQuimica.Show();
            this.Hide();
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            Sobre formQuimica = new Sobre();
            formQuimica.Show();
            this.Hide();
        }

        private void btnVarios_Click(object sender, EventArgs e)
        {
            Varios formQuimica = new Varios();
            formQuimica.Show();
            this.Hide();
        }

        private void dgvOrina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}

