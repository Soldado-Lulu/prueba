
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

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            // Validar que el Nombre y Apellido no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete al menos el Nombre y Apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener la fecha del DateTimePicker
            string fechaRegistro = dtpFecha.Value.ToString("yyyy-MM-dd");

            // Validar y asignar valores numéricos (cuenta, porcentaje)
            float cuenta = 0, porcentaje = 0, saldoMedico = 0, saldoLab = 0;

            // Intentar convertir valores numéricos, si falla, asignar 0
            float.TryParse(txtCuenta.Text, out cuenta);
            float.TryParse(txtPorcentaje.Text, out porcentaje);

            // Calcular saldos solo si cuenta y porcentaje son válidos
            saldoMedico = cuenta * porcentaje / 100;
            saldoLab = cuenta - saldoMedico;

            // Crear objeto PacienteM con datos y asignar valores vacíos si no hay datos
            PacienteM nuevoPaciente = new PacienteM()
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                ApellidoM =txtApellidoM.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? "Sin Teléfono" : txtTelefono.Text.Trim(),
                Edad = string.IsNullOrWhiteSpace(txtEdad.Text) ? "Sin Edad" : txtEdad.Text.Trim(),
                Medico = string.IsNullOrWhiteSpace(txtMedico.Text) ? "No Asignado" : txtMedico.Text.Trim(),
                Fecha = fechaRegistro,
                Cuenta = cuenta,
                Porcentaje = porcentaje,
                SaldoMedico = saldoMedico,
                SaldoLab = saldoLab
            };

            // ⚡️ Guardar el nuevo paciente y obtener el ID generado
            int idPaciente = PacienteLogica.Instancia.GuardarYDevolverId(nuevoPaciente);

            if (idPaciente > 0)
            {
                // Establecer el nuevo paciente activo
                nuevoPaciente.IdPaciente = idPaciente;
                PacienteActivo.EstablecerPaciente(nuevoPaciente);

                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el formulario de Orina con el ID correcto del paciente
                Orina formOrina = new Orina(idPaciente);
                formOrina.Show();
                this.Hide(); // Cierra el formulario actual para abrir el nuevo
            }
            else
            {
                MessageBox.Show("Error al registrar el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Reporte formQuimica = new Reporte();
            formQuimica.Show();
            this.Hide();
        }
    }
}

