using prueba.Logica;
using Prueba.Modelo;
using System;
using System.Data;
using System.Windows.Forms;

namespace prueba.Vista
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
            LlenarDataGridView();
        }

        private void Reporte_Load(object sender, EventArgs e) { }

        private void LlenarDataGridView()
        {
            dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
        }

        private void dgvOrina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ✅ Obtener ID del paciente
                int idPaciente = Convert.ToInt32(dgvOrina.Rows[e.RowIndex].Cells["ID Paciente"].Value);

                // ✅ Obtener el nombre de la columna (tipo de examen)
                string nombreExamen = dgvOrina.Columns[e.ColumnIndex].HeaderText.Trim();

                // ✅ Validar que haya un valor en la celda
                string valorCelda = dgvOrina.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (string.IsNullOrWhiteSpace(valorCelda))
                    return;

                // ✅ Buscar el paciente en base al ID
                PacienteM paciente = PacienteLogica.Instancia.BuscarPorId(idPaciente);
                if (paciente == null)
                {
                    MessageBox.Show("No se encontró el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Establecer paciente activo global
                PacienteActivo.EstablecerPaciente(paciente);

                // ✅ Abrir el formulario correspondiente al examen
                switch (nombreExamen)
                {
                    case "Orina":
                        new Orina(idPaciente).Show();
                        this.Close(); // en lugar de this.Hide();

                        break;
                    case "Quimica":
                        new Quimica(idPaciente).Show();
                        break;
                    case "Hemograma":
                        new Hemograma(idPaciente).Show();
                        this.Close(); // en lugar de this.Hide();

                        break;
                    case "Serologia":
                        new Serologia(idPaciente).Show();
                        break;
                    case "HCG":
                        new HCG(idPaciente).Show();
                        break;
                    case "Coproparasitologia":
                        new Copros(idPaciente).Show();
                        break;
                    case "Microbiologia":
                        new Micro(idPaciente).Show();
                        break;
                    case "Varios":
                        new Varios(idPaciente).Show();
                        break;
                    case "Blanco":
                        new Blanco(idPaciente).Show();
                        break;
                    default:
                        MessageBox.Show("Seleccione un examen válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
        }
    }
}
