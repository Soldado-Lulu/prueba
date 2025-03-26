
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
            PacienteLogica.Instancia.ListarPacientes(dgvPaciente);

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
            // Verificar solo los campos realmente obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete al menos el Nombre y Apellido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar la fecha del DateTimePicker
            string fechaRegistro = dtpFecha.Value.ToString("yyyy-MM-dd");

            // Convertir Cuenta y Porcentaje a valores numéricos (opcional)
            decimal? cuenta = null;
            decimal? porcentaje = null;

            if (decimal.TryParse(txtCuenta.Text, out decimal cuentaValor))
            {
                cuenta = cuentaValor;
            }

            if (decimal.TryParse(txtPorcentajeMedico.Text, out decimal porcentajeValor))
            {
                porcentaje = porcentajeValor;
            }

            // Crear el objeto PacienteM con todos los campos opcionales
            PacienteM nuevoPaciente = new PacienteM()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text,
                Edad = string.IsNullOrWhiteSpace(txtEdad.Text) ? null : txtEdad.Text,
                Medico = string.IsNullOrWhiteSpace(txtMedico.Text) ? null : txtMedico.Text,
                Fecha = fechaRegistro,
                Cuenta = cuenta,
                Porcentaje = porcentaje
            };

            // Calcular los saldos antes de guardar
            logica.CalcularSaldo(nuevoPaciente);

            int nuevoId = PacienteLogica.Instancia.GuardarYObtenerId(nuevoPaciente);

            if (nuevoId > 0)
            {
                nuevoPaciente.IdPaciente = nuevoId; // asigna el ID al objeto
                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PacienteActivo.EstablecerPaciente(nuevoPaciente);

                Hemograma formHemograma = new Hemograma(nuevoId); // 👈 Aquí puedes abrir Hemograma directamente
                formHemograma.Show();
                this.Close(); // en lugar de this.Hide();
            }
            else
            {
                MessageBox.Show("Error al registrar el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reporte formQuimica = new Reporte();
            formQuimica.Show();
            this.Hide();
        }
    }
}

