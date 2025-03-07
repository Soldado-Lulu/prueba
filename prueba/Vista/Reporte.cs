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
                        break;
                    case "Quimica":
                        Quimica formQuimica = new Quimica(idPaciente);
                        formQuimica.Show();
                        break;
                    case "Hemograma":
                        Hemograma formHemograma = new Hemograma(idPaciente);
                        formHemograma.Show();
                        break;
                    case "Serologia":
                        Serologia formSerologia = new Serologia(idPaciente);
                        formSerologia.Show();
                        break;
                    case "HCG":
                        HCG formHCG = new HCG(idPaciente);
                        formHCG.Show();
                        break;
                    case "Coproparasitologia":
                        Copros formCopro = new Copros(idPaciente);
                        formCopro.Show();
                        break;
                    case "Microbiologia":
                        Micro formMicro = new Micro(idPaciente);
                        formMicro.Show();
                        break;
                    case "Varios":
                        Varios formVarios = new Varios(idPaciente);
                        formVarios.Show();
                        break;
                    case "Blanco":
                        Blanco formBlanco = new Blanco(idPaciente);
                        formBlanco.Show();
                        break;
                    default:
                        MessageBox.Show("Seleccione un examen válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
        }



    }
}

