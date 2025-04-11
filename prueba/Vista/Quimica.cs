using Laboratorio.Vista;
using prueba.Logica;
using prueba.Logica_Sevicio;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba.Vista
{
    public partial class Quimica : Form
    {
        private PacienteM pacienteActivo;

        public Quimica()
        {
            InitializeComponent();
        }
        private int idPaciente;
        public Quimica(int id)
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
                QuimicaM examen = QuimicaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    txtGlucosa.Text = examen.Glucosa;
                    txtUrea.Text = examen.Urea;
                    txtCreatinina.Text = examen.Creatina;
                    txtBUN.Text = examen.BUN;
                    txtUrico.Text = examen.Urico;
                    txtColesterol.Text = examen.Colesterol;
               //     txtHDL.Text = examen.HDL;
                    txtag.Text = examen.LDL;
                  //  txtTrigliceridos.Text = examen.Triglicerido;
                    txtBilirrubina.Text = examen.Bilirrubina;
                    txtDirecta.Text = examen.Directa;
                    txtIndirecta.Text = examen.Indirecta;
                    
                    txtGOT.Text = examen.GOT;
                    txtGPT.Text = examen.GPT;
                    txtFosfatasaAlcalina.Text = examen.FosfatasaAlcalina;
                    txtAmilasa.Text = examen.Amilasa;
                    txtProteina.Text = examen.Proteina;
                    txtAlbumina.Text = examen.Albumina;
                    txtglobulina.Text = examen.Globulina;
                    txtRelacion.Text = examen.Relacion;
                    txtFosfatasaAcida.Text = examen.FosfatasaAcida;
                    txtFosfatasaAcidaProstatica.Text = examen.FosfatasaAcidaProstatica;
                    txtCKMB.Text = examen.CKMB;
                    txtCPK.Text = examen.CPK;
                    txtHemoglobina.Text = examen.Hemoglobina;
      
                }
            }
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            if (pacienteActivo == null)
            {
                MessageBox.Show("No hay un paciente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear un objeto QuimicaM con los valores de los controles de texto
            QuimicaM objeto = new QuimicaM()
                {
                    Glucosa = txtGlucosa.Text,
                    Urea = txtUrea.Text,
                    Creatina = txtCreatinina.Text,
                    BUN = txtBUN.Text,
                    Urico =txtUrico.Text,
                    Colesterol = txtColesterol.Text,
                    //HDL = txtHDL.Text,
                    LDL = txtag.Text,
                //    Triglicerido = txtTrigliceridos.Text,
                    Bilirrubina = txtBilirrubina.Text,
                    Directa = txtDirecta.Text,
                    Indirecta = txtIndirecta.Text,
                    //Total = txtTotal.Text,
                    GOT = txtGOT.Text,
                    GPT = txtGPT.Text,
                    FosfatasaAlcalina = txtFosfatasaAlcalina.Text,
                    Amilasa = txtAmilasa.Text,
                    Proteina = txtProteina.Text,
                    Albumina = txtAlbumina.Text,
                    Globulina = txtglobulina.Text,
                    Relacion = txtRelacion.Text,
                    FosfatasaAcida = txtFosfatasaAcida.Text,
                    FosfatasaAcidaProstatica = txtFosfatasaAcidaProstatica.Text,
                    CKMB = txtCKMB.Text,
                    CPK = txtCPK.Text,
                    Hemoglobina = txtHemoglobina.Text,
                    
                  
                   
                };
            int idPaciente = pacienteActivo.IdPaciente;
            bool existe = QuimicaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;
            bool resultado = existe ?
                QuimicaLogica.Instancia.ActualizarExamen(objeto, idPaciente) :
                QuimicaLogica.Instancia.GuardarExamen(objeto, idPaciente);

            if (resultado)
            {
                MessageBox.Show(existe ? "Examen actualizado correctamente." : "Examen guardado correctamente.", "Éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar o actualizar el examen.", "Error");
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


        private void Quimica_Load(object sender, EventArgs e)
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
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}  {pacienteActivo.ApellidoM}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;
            }
            else
            {
                MessageBox.Show("No hay un paciente activo. Registre un paciente antes de continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

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
                    txtHemoglobina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           using(ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtUrea.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtCreatinina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtBUN.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtUrico.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

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

        private void button11_Click(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtBilirrubina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtDirecta.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
                using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtIndirecta.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtGOT.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtGPT.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtCKMB.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtAmilasa.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtProteina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtAlbumina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtAmilasa.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
               using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtDirecta.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
                using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtBilirrubina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
               using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtProteina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
               using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtAlbumina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
             using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtglobulina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtRelacion.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
               using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtFosfatasaAcidaProstatica.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtIndirecta.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtGOT.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtGPT.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtCKMB.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
              using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtCPK.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
               using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtColesterol.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
                using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtag.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }

     

        private void button23_Click_1(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()) // Crea un selector de color
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) // Si el usuario elige un color
                {
                    txtFosfatasaAlcalina.ForeColor = colorDialog.Color; // Cambia el color del texto en el TextBox
                }

            }
        }
    }
}
