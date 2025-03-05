using prueba.Logica;
using prueba.Logica_Sevicio;
using prueba.Modelo;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void Blanco_Load(object sender, EventArgs e)
        {
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

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            BlancoM objeto = new BlancoM()
            {
                Muestra = txtMuestra.Text,
                Examen = txtExamen.Text,
                Datos = txtDatos.Text,
                Otros = txtOtros.Text,
            };

            int idPaciente = pacienteActivo.IdPaciente; // Usamos la variable global

            bool guardado = BlancoLogica.Instancia.GuardarExamen(objeto, idPaciente);

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
    }
}
