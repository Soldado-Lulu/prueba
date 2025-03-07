
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using prueba.Modelo; // Asegúrate de que la clase PacienteM está en este namespace
using prueba.Logica;
using prueba.Vista;
using Prueba.Modelo;
namespace Laboratorio.Vista
{
    public partial class RegistroPaciente : Form
    {
        private PacienteLogica logica;
        public RegistroPaciente()
        {
            InitializeComponent();
            logica = new PacienteLogica();
            CargarPacientes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verificar solo los campos realmente obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete al menos el Nombre y Apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar la fecha del DateTimePicker

            string fechaRegistro = dtpFecha.Value.ToString("yyyy-MM-dd");

            // Crear el objeto PacienteM con campos opcionales
            PacienteM nuevoPaciente = new PacienteM()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text,
                Edad = string.IsNullOrWhiteSpace(txtEdad.Text) ? null : txtEdad.Text,
                Medico = string.IsNullOrWhiteSpace(txtMedico.Text) ? null : txtMedico.Text,
                Fecha = fechaRegistro // Nueva propiedad en la clase
            };

            bool guardado = logica.Guardar(nuevoPaciente);

            if (guardado)
            {
                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Asignar los datos del paciente a la clase PacienteActivo
                PacienteActivo.EstablecerPaciente(nuevoPaciente);

                // Abrir la interfaz de examen
                Orina formOrina = new Orina();
                formOrina.Show();
                this.Hide(); // Ocultar la ventana actual
            }
            else
            {
                MessageBox.Show("Error al registrar el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPacientes()
        {
            List<PacienteM> listaPacientes = logica.ObtenerTodos();
            dgvPaciente.DataSource = listaPacientes; // Mostrar en el DataGridView
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEdad.Clear();
            txtMedico.Clear();
        }

        private void RegistroPaciente_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;

        }
    }
}

