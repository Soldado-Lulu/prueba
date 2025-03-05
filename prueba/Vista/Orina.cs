using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void Orina_Load(object sender, EventArgs e)
        {
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
        }

        private void btnBlanco_Click(object sender, EventArgs e)
        {
            Blanco formQuimica = new Blanco();
            formQuimica.Show();
            this.Hide();
        }
    }
}

