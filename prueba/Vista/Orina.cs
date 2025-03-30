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
            pacienteActivo = PacienteLogica.Instancia.BuscarPorId(idPaciente);

            if (pacienteActivo != null)
            {
                // Mostrar datos del paciente
                panel1.Visible = true;
                lblNombreCompleto.Visible = true;
                lblEdad.Visible = true;
                lblMedico.Visible = true;

                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;

                // Cargar datos del examen si existen
                OrinaM examen = OrinaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    txtAspecto.Text = examen.Aspecto;
                    txtColor.Text = examen.Color;
                    txtOlor.Text = examen.Olor;
                    txtDensidad.Text = examen.Densidad;
                    txtReaccion.Text = examen.Reaccion;
                    txtGlucosa.Text = examen.Glucosa;
                    txtBilirrubina.Text = examen.Bilirrubina;
                    txtCetonas.Text = examen.Cetonas;
                    txtSangre.Text = examen.Sangre;
                    txtProteina.Text = examen.Proteina;
                    txtUrobiliogeno.Text = examen.Urobiliogeno;
                    txtNitrito.Text = examen.Nitrito;
                    txtLeucocitos.Text = examen.Leucocito1;
                    txtEritrocitos.Text = examen.Eritrocito;
                    txtLeucocitos1.Text = examen.Leucocito2;
                    txtCED.Text = examen.CED;
                    txtRedondas.Text = examen.Redonda;
                    txtEmbarazo.Text = examen.Embarazo;
                    txtOtros.Text = examen.Otros;
                    txtObservaciones.Text = examen.Observaciones;
                    txtFlora.Text = examen.Flora;
                    txtPiocitos.Text = examen.Piocito;
                    txtCristales.Text = examen.Cristale;
                    txtCilindros.Text = examen.Cilindro;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron datos del paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Orina_Load(object sender, EventArgs e)
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
            //dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
        }
     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            if (pacienteActivo == null)
            {
                MessageBox.Show("No hay un paciente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            int idPaciente = pacienteActivo.IdPaciente;

            // Verificar si ya existe un examen para este paciente
            bool existe = OrinaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;
            bool operacionExitosa = false;

            if (existe)
            {
                operacionExitosa = OrinaLogica.Instancia.ActualizarExamen(examenOrina, idPaciente);
            }
            else
            {
                operacionExitosa = OrinaLogica.Instancia.GuardarExamen(examenOrina, idPaciente);
            }

            if (operacionExitosa)
            {
                MessageBox.Show("Examen de orina guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

      

        private void dgvOrina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNuevoPAciente_Click_1(object sender, EventArgs e)
        {
            RegistroPaciente formQuimica = new RegistroPaciente();
            formQuimica.Show();
            this.Hide();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
               using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtColor.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                 using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtGlucosa.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtBilirrubina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void btnOrina_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Hemograma formHemograma = new Hemograma(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHemograma_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Quimica formHemograma = new Quimica(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVarios_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Varios formHemograma = new Varios(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBlanco_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Blanco formHemograma = new Blanco(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCopros_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Copros formHemograma = new Copros(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHCG_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                HCG formHemograma = new HCG(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSerologia_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Serologia formHemograma = new Serologia(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMicro_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Micro formHemograma = new Micro(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSobre_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Sobre formHemograma = new Sobre(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

