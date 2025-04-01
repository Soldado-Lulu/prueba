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

            pacienteActivo = PacienteLogica.Instancia.BuscarPorId(idPaciente);
            if (pacienteActivo != null)
            {
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;
                panel1.Visible = true;

                VariosM examen = VariosLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    // txtMuestra.Text = examen.Muestra;
                    ///txtExamen.Text = examen.Examen;
                    txtDatos.Text = examen.Datos;
                    richTextBox1.Text = examen.Otros;
                    txtValoresReferencia.Text = examen.Valores;
                    txtPaciente.Text = examen.Paciente;

                }
            }
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
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}  {pacienteActivo.ApellidoM}";
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
                MessageBox.Show("No hay un paciente activo para guardar el examen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idPaciente = pacienteActivo.IdPaciente;

            VariosM objeto = new VariosM()
            {
                //Muestra = txtMuestra.Text,
                // Examen = /txtExamen.Text,
                Datos = txtDatos.Text,
                Otros = richTextBox1.Text,
                Valores = txtValoresReferencia.Text,
                Paciente = txtPaciente.Text
            };



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
            bool existe = VariosLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;

            bool resultado = existe
                ? VariosLogica.Instancia.ActualizarExamen(objeto, idPaciente)
                : VariosLogica.Instancia.GuardarExamen(objeto, idPaciente);

            if (resultado)
            {
                MessageBox.Show(existe ? "Examen actualizado correctamente." : "Examen guardado correctamente.", "Éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar o actualizar el examen de varios.", "Error");
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
                Hemograma form = new Hemograma(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
        }
        private void MostrarAdvertencia()
        {
            MessageBox.Show("No hay un paciente activo. Registre o seleccione uno antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Blanco form = new Blanco(pacienteActivo.IdPaciente);
                form.Show();
                this.Hide();
            }
            else MostrarAdvertencia();
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
            if (txtPaciente.SelectionLength > 0) // Verifica que haya texto seleccionado
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Aplica el color al texto seleccionado
                        txtPaciente.SelectionColor = colorDialog.Color;
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
            if (txtValoresReferencia.SelectionLength > 0) // Verifica que haya texto seleccionado
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Aplica el color al texto seleccionado
                        txtValoresReferencia.SelectionColor = colorDialog.Color;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona primero el texto que deseas colorear.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
    }
}
