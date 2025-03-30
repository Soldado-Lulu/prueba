using Laboratorio.Vista;
using prueba.Logica;
using prueba.Logica_Sevicio;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba.Vista
{
    public partial class Blanco : Form
    {
        private PacienteM pacienteActivo;

        public Blanco()
        {

            InitializeComponent();
           // string tipoExamen = "orina"; // Cambia esto si es necesario
         //   CargarDatosEnDataGridView(tipoExamen);

        }
  

        private int idPaciente;
        public Blanco(int id)
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

                BlancoM examen = BlancoLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                   // txtMuestra.Text = examen.Muestra;
///txtExamen.Text = examen.Examen;
                    txtDatos.Text = examen.Datos;
                    richTextBox1.Text = examen.Otros;
                    txtReferencia.Text = examen.Valores;
                    txtpaciente.Text = examen.Paciente;
                }
            }
        }

        private void Blanco_Load(object sender, EventArgs e)
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

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            if (pacienteActivo == null)
            {
                MessageBox.Show("No hay un paciente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            BlancoM objeto = new BlancoM()
                {
                    //Muestra = txtMuestra.Text,
                   // Examen = /txtExamen.Text,
                    Datos = txtDatos.Text,
                    Otros = richTextBox1.Text,
                    Valores = txtReferencia.Text,
                    Paciente = txtpaciente.Text
                };

                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores de null

                
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
            bool existe = BlancoLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;

            bool resultado = existe
                ? BlancoLogica.Instancia.ActualizarExamen(objeto, idPaciente)
                : BlancoLogica.Instancia.GuardarExamen(objeto, idPaciente);

            if (resultado)
            {
                MessageBox.Show(existe ? "Examen actualizado correctamente." : "Examen guardado correctamente.", "Éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar o actualizar el examen de Blanco.", "Error");
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
                Hemograma form = new Hemograma(pacienteActivo.IdPaciente);
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
                Copros form = new Copros(pacienteActivo.IdPaciente);
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
                Quimica form = new Quimica(pacienteActivo.IdPaciente);
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
            MessageBox.Show("No hay un paciente activo. Registre o seleccione uno antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void btnNuevoPaciente_Click_1(object sender, EventArgs e)
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

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBlanco_Click_1(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
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

        private void btnOrina_Click_1(object sender, EventArgs e)
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

        private void btnCopros_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Copros form = new Copros(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else
            {
                MostrarAdvertencia();
            }
        }

        private void btnHCG_Click_1(object sender, EventArgs e)
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

        private void btnSerologia_Click_1(object sender, EventArgs e)
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

        private void btnMicro_Click_1(object sender, EventArgs e)
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

        private void btnSobre_Click_1(object sender, EventArgs e)
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

      

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnVarios_Click_1(object sender, EventArgs e)
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

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) // Verifica que haya texto seleccionado
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Aplica el color al texto seleccionado
                        richTextBox1.SelectionColor = colorDialog.Color;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona primero el texto que deseas colorear.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtDatos.SelectionLength > 0) // Verifica que haya texto seleccionado
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Aplica el color al texto seleccionado
                        txtDatos.SelectionColor = colorDialog.Color;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona primero el texto que deseas colorear.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtpaciente.SelectionLength > 0) // Verifica que haya texto seleccionado
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Aplica el color al texto seleccionado
                        txtpaciente.SelectionColor = colorDialog.Color;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona primero el texto que deseas colorear.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtReferencia.SelectionLength > 0) // Verifica que haya texto seleccionado
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Aplica el color al texto seleccionado
                        txtReferencia.SelectionColor = colorDialog.Color;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona primero el texto que deseas colorear.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
