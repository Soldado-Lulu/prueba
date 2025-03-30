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
    public partial class Hemograma : Form
    {
        private PacienteM pacienteActivo;


        private void CargarDatos()
        {
            pacienteActivo = PacienteLogica.Instancia.BuscarPorId(idPaciente);

            if (pacienteActivo != null)
            {
                panel1.Visible = true;
                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;

                // Cargar datos del examen si existen
                HemogramaM examen = HemogramaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);
                if (examen != null)
                {
                    //txtEritrocitos.Text = examen.Eritrocitos;
                    txtLeucocitos.Text = examen.Leucocitos;
                    txtHemoglobina.Text = examen.Hemoglobina;
                    txtHematocritos.Text = examen.Hematocrito;
                    txtPlaquetas.Text = examen.Plaquetas;
                    textMielocitos.Text = examen.Mielocitos;
                    textMetamielocitos.Text = examen.Melamielocitos;
                    textCayados.Text = examen.Cayados;
                    textSegmentados.Text = examen.Segmentados;
                    textLinfocitos.Text = examen.Linfocitos;
                    textMonocitos.Text = examen.Monocitos;
                    textEosinofilos.Text = examen.Eosinofilos;
                    textBasofilos.Text = examen.Basofilos;
                    textVES1.Text = examen.VES1;
                    textVES2.Text = examen.VES2;
                    textIK.Text = examen.Ik;
                    textGrupoSanguineo.Text = examen.GrupoSanguineo;
                    textFactorRh.Text = examen.Factor;
                    txtTiempoSangria.Text = examen.TiempoSangria;
                    txtTiempoCoagulacion.Text = examen.TiempoCoagulacion;
                    txtTiempoProtrombina.Text = examen.TiempoProtrombina;
                    txtPorcentajeActividad.Text = examen.PorcentajeActividad;
                    txtAPTT.Text = examen.Aptt;
                    txtSerieRoja.Text = examen.SerieRoja;
                    txtSerieBlanca.Text = examen.SerieBlanca;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron datos del paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public Hemograma()
        {
            InitializeComponent();
        }
        private int idPaciente;

        public Hemograma(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
            txtHematocritos.TextChanged += ActualizarResultado;

        }
        private const float VARIABLE = 110000;
        private void ActualizarResultado(object sender, EventArgs e)
        {
            // Verifica si el valor ingresado es un número válido
            if (float.TryParse(txtHematocritos.Text, out float valor))
            {
                // Multiplica el valor ingresado por la variable fija
                float resultado = valor * VARIABLE;

                // Muestra el resultado en el Label
                lblEritrocitos.Text = $"{resultado}";
            }
            else
            {
                // Mensaje si el valor no es válido
                lblEritrocitos.Text = "";
            }
        }


        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            HemogramaM objeto = new HemogramaM()
            {
                Eritrocitos = lblEritrocitos.Text,
                Leucocitos = txtLeucocitos.Text,
                Hemoglobina = txtHemoglobina.Text,
                Hematocrito = txtHematocritos.Text,
                Plaquetas = txtPlaquetas.Text,
                Mielocitos = textMielocitos.Text,
                Melamielocitos = textMetamielocitos.Text,
                Cayados = textCayados.Text,
                Segmentados = textSegmentados.Text,
                Linfocitos = textLinfocitos.Text,
                Monocitos = textMonocitos.Text,
                Eosinofilos = textEosinofilos.Text,
                Basofilos = textBasofilos.Text,
                VES1 = textVES1.Text,
                VES2 = textVES2.Text,
                Ik = textIK.Text,
                GrupoSanguineo = textGrupoSanguineo.Text,
                Factor = textFactorRh.Text,
                TiempoSangria = txtTiempoSangria.Text,
                TiempoCoagulacion = txtTiempoCoagulacion.Text,
                TiempoProtrombina = txtTiempoProtrombina.Text,
                PorcentajeActividad = txtPorcentajeActividad.Text,
                Aptt = txtAPTT.Text,
                SerieRoja = txtSerieRoja.Text,
                SerieBlanca = txtSerieBlanca.Text
            };

            if (idPaciente <= 0)
            {
                MessageBox.Show("ID de paciente inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool existe = HemogramaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente) != null;
            bool resultado = existe ?
                HemogramaLogica.Instancia.ActualizarExamen(objeto, idPaciente) :
                HemogramaLogica.Instancia.GuardarExamen(objeto, idPaciente);

            if (resultado)
            {
                MessageBox.Show("Examen de Hemograma guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Error al guardar o actualizar el examen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void Hemograma_Load(object sender, EventArgs e)
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

       
        private void btnNuevoPaciente_Click_1(object sender, EventArgs e)
        {
            RegistroPaciente formQuimica = new RegistroPaciente();
            formQuimica.Show();
            this.Hide();
        }

       

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void btnOrina_Click(object sender, EventArgs e)
        {
            if (pacienteActivo != null)
            {
                Orina formHemograma = new Orina(pacienteActivo.IdPaciente); // ✅ PASA EL ID
                formHemograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No hay un paciente cargado para continuar al Hemograma.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCopros_Click(object sender, EventArgs e)
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

        private void btnHCG_Click(object sender, EventArgs e)
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

        private void btnSerologia_Click(object sender, EventArgs e)
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

        private void btnMicro_Click(object sender, EventArgs e)
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

        private void btnBlanco_Click(object sender, EventArgs e)
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

        private void btnSobre_Click(object sender, EventArgs e)
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

        private void btnVarios_Click(object sender, EventArgs e)
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

    }
}
