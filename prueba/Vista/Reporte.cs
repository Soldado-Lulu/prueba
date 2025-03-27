using Laboratorio.Vista;
using prueba.Logica;
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
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();

            LlenarDataGridView();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
     
            cmbMedico.Items.Clear();
            cmbMedico.Items.Add("Todos");

            var medicos = PacienteLogica.Instancia.ObtenerMedicosUnicos();
            foreach (var medico in medicos)
            {
                cmbMedico.Items.Add(medico);
            }

            cmbMedico.SelectedIndex = 0;
        

        CargarMedicos();
            LlenarDataGridView();
        }

        private void CargarMedicos()
        {
            List<string> medicos = PacienteLogica.Instancia.ObtenerMedicosUnicos();
            cmbMedico.Items.Clear();
            cmbMedico.Items.Add("Todos");
            cmbMedico.Items.AddRange(medicos.ToArray());
            cmbMedico.SelectedIndex = 0;
        }

        private void LlenarDataGridView()
        {
            dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
        }




        private void dgvOrina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verifica que se haga clic en una celda válida
            {
                // Obtén el ID del paciente de la fila seleccionada
                int idPaciente = Convert.ToInt32(dgvOrina.Rows[e.RowIndex].Cells["ID Paciente"].Value);

                // Obtén el nombre del examen desde el encabezado de la columna
                string nombreExamen = dgvOrina.Columns[e.ColumnIndex].HeaderText.Trim(); // Usamos Trim() para eliminar espacios extras

                // Mostrar el valor del examen para depuración
                MessageBox.Show($"ID del Paciente: {idPaciente}\nNombre del Examen: '{nombreExamen}'");

                // Asegúrate de que solo se muestren los datos de este paciente para el examen específico
                switch (nombreExamen)
                {
                    case "Orina":
                        // Se pasa el ID del paciente y el examen específico
                        Orina formOrina = new Orina(idPaciente);
                        formOrina.Show();
                        this.Hide();
                        break;
                    case "Quimica":
                        Quimica formQuimica = new Quimica(idPaciente);
                        formQuimica.Show();
                        this.Hide();
                        break;
                    case "Hemograma":
                        Hemograma formHemograma = new Hemograma(idPaciente);
                        formHemograma.Show();
                        this.Hide();
                        break;
                    case "Serologia":
                        Serologia formSerologia = new Serologia(idPaciente);
                        formSerologia.Show();
                        this.Hide();
                        break;
                    case "HCG":
                        HCG formHCG = new HCG(idPaciente);
                        formHCG.Show();
                        this.Hide();
                        break;
                    case "Coproparasitologia":
                        Copros formCopro = new Copros(idPaciente);
                        formCopro.Show();
                        this.Hide();
                        break;
                    case "Microbiologia":
                        Micro formMicro = new Micro(idPaciente);
                        formMicro.Show();
                        this.Hide();
                        break;
                    case "Varios":
                        Varios formVarios = new Varios(idPaciente);
                        formVarios.Show();
                        this.Hide();
                        break;
                    case "Blanco":
                        Blanco formBlanco = new Blanco(idPaciente);
                        formBlanco.Show();
                        this.Hide();
                        break;
                    default:
                        MessageBox.Show("Seleccione un examen válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
        }

        private void cmbMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            string seleccion = cmbMedico.SelectedItem.ToString();
            if (seleccion == "Todos")
            {
                dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
            }
            else
            {
                dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesPorMedico(seleccion);
            }

            if (dgvOrina.Columns.Contains("Monto Total"))
            {
                float total = 0;
                foreach (DataGridViewRow row in dgvOrina.Rows)
                {
                    if (row.Cells["Monto Total"].Value != null && float.TryParse(row.Cells["Monto Total"].Value.ToString(), out float valor))
                    {
                        total += valor;
                    }
                }
                lblTotalDoctor.Text = $"Total ingresos: Bs. {total:N2}";
            }
            else
            {
                lblTotalDoctor.Text = "Columna 'Monto Total' no disponible.";
            }


        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            RegistroPaciente formQuimica = new RegistroPaciente();
            formQuimica.Show();
            this.Hide();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

      
            private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string medicoSeleccionado = cmbMedico.SelectedItem?.ToString() ?? "";
            DateTime fechaDesde = dtpDesde.Value.Date;
            DateTime fechaHasta = dtpHasta.Value.Date;

            if (medicoSeleccionado == "Todos")
            {
                dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes(); // sin filtro
            }
            else
            {
                dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesPorMedicoYFecha(medicoSeleccionado, fechaDesde, fechaHasta);
            }

            // Calcular total si hay columna "Monto Total"
            if (dgvOrina.Columns.Contains("Monto Total"))
            {
                float total = 0;
                foreach (DataGridViewRow row in dgvOrina.Rows)
                {
                    if (row.Cells["Monto Total"].Value != null && float.TryParse(row.Cells["Monto Total"].Value.ToString(), out float valor))
                    {
                        total += valor;
                    }
                }
                lblTotalDoctor.Text = $"Total ingresos: Bs. {total:N2}";
            }
            else
            {
                lblTotalDoctor.Text = "Columna 'Monto Total' no disponible.";
            }
        }

    }
}


