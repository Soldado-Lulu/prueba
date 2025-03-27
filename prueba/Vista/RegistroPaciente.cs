
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
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete al menos el Nombre y Apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fechaRegistro = dtpFecha.Value.ToString("yyyy-MM-dd");

            float cuenta = float.Parse(txtCuenta.Text);
            float porcentaje = float.Parse(txtPorcentaje.Text);
            float saldoMedico = cuenta * porcentaje / 100;
            float saldoLab = cuenta - saldoMedico;

            PacienteM nuevoPaciente = new PacienteM()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text,
                Edad = string.IsNullOrWhiteSpace(txtEdad.Text) ? null : txtEdad.Text,
                Medico = string.IsNullOrWhiteSpace(txtMedico.Text) ? null : txtMedico.Text,
                Fecha = fechaRegistro,
                Cuenta = cuenta,
                Porcentaje = porcentaje,
                SaldoMedico = saldoMedico,
                SaldoLab = saldoLab
            };


            // ⚠️ Usamos el nuevo método que devuelve el ID
            int idPaciente = logica.GuardarYDevolverId(nuevoPaciente);

            if (idPaciente > 0)
            {
                nuevoPaciente.IdPaciente = idPaciente;
                PacienteActivo.EstablecerPaciente(nuevoPaciente);

                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el formulario de examen con el ID correcto
                Orina formOrina = new Orina(idPaciente);
                formOrina.Show();
                this.Hide();
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

