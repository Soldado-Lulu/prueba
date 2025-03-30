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

            if (dgvOrina.Rows.Count > 0)  // Verifica si hay filas antes de aplicar ajustes
            {
                // ✅ Ocultar la columna "ID Paciente"
                if (dgvOrina.Columns.Contains("ID Paciente"))
                {
                    dgvOrina.Columns["ID Paciente"].Visible = false;
                }
                if (dgvOrina.Columns.Contains("Monto"))
                {
                    dgvOrina.Columns["Monto"].DefaultCellStyle.Format = "N2"; // Formato con 2 decimales
                }
                // Ajustar automáticamente las filas y columnas para adaptarse al contenido
                dgvOrina.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvOrina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Desactivamos el ajuste global

                // Ajustar el ancho de las columnas de la primera fila según el contenido
                for (int i = 0; i < dgvOrina.Columns.Count; i++)
                {
                    dgvOrina.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Ajustar columnas
                }

                // Restablecer AutoSizeMode para mantener el ancho ajustado
                for (int i = 0; i < dgvOrina.Columns.Count; i++)
                {
                    int colWidth = dgvOrina.Columns[i].Width; // Obtener el ancho ajustado
                    dgvOrina.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Desactivar autoajuste
                    dgvOrina.Columns[i].Width = colWidth; // Aplicar el nuevo ancho
                }
                AjustarTamañoDeLetra();
                // Opcional: ajustar la altura de la primera fila para adaptarse al contenido
                if (dgvOrina.Rows.Count > 0)
                {
                    dgvOrina.Rows[0].Height = dgvOrina.Rows[0].GetPreferredHeight(0, DataGridViewAutoSizeRowMode.AllCells, true);
                }
            }
        }

        // Método para ajustar el tamaño de letra dinámicamente
        private void AjustarTamañoDeLetra()
        {
            int maxWidth = dgvOrina.Width / dgvOrina.Columns.Count; // Ancho promedio de columna
            int maxHeight = dgvOrina.Rows[0].Height; // Altura de la primera fila

            // Establecer un tamaño de letra máximo y mínimo
            float fontSize = 12; // Tamaño predeterminado
            float maxFontSize = 22; // Máximo tamaño de fuente
            float minFontSize = 11;  // Mínimo tamaño de fuente

            // Establecer un bucle para encontrar el mejor tamaño de letra
            using (Graphics g = dgvOrina.CreateGraphics())
            {
                for (float size = maxFontSize; size >= minFontSize; size--)
                {
                    Font testFont = new Font("Segoe UI", size);
                    SizeF textSize = g.MeasureString("Texto de Prueba", testFont);

                    if (textSize.Width <= maxWidth && textSize.Height <= maxHeight)
                    {
                        fontSize = size;
                        break;
                    }
                }
            }

            // Aplicar el tamaño ajustado a todo el DataGridView
            dgvOrina.Font = new Font("Segoe UI", fontSize);
            dgvOrina.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            dgvOrina.DefaultCellStyle.Font = new Font("Segoe UI", fontSize);

            // Ajustar la altura de las filas nuevamente después del cambio de fuente
            dgvOrina.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

      
     

        private void dgvOrina_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            panel1.BackColor = Color.Black; // Cambia color de fondo para parecer barra

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

        private void cmbMedico_SelectedIndexChanged_1(object sender, EventArgs e)
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

            // Actualizar el total después de cambiar el médico
            ActualizarTotalMonto();
        }


        private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void btnFiltrar_Click_1(object sender, EventArgs e)
        {
     
            DateTime fechaDesde = dtpDesde.Value.Date;
            DateTime fechaHasta = dtpHasta.Value.Date;

            if (fechaDesde > fechaHasta)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", "Error de Fecha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los datos filtrados por fecha
            DataTable dtFiltrado = PacienteLogica.Instancia.ObtenerPacientesPorFecha(fechaDesde, fechaHasta);
            dgvOrina.DataSource = dtFiltrado;

            if (dtFiltrado.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos en el rango de fechas seleccionado.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Actualizar el total de "Monto Total" después de filtrar
            ActualizarTotalMonto();
        }

        private void ActualizarTotalMonto()
        {
            if (dgvOrina.Rows.Count > 0 && dgvOrina.Columns.Contains("Monto"))
            {
                float total = 0;
                foreach (DataGridViewRow row in dgvOrina.Rows)
                {
                    if (row.Cells["Monto"].Value != null && float.TryParse(row.Cells["Monto"].Value.ToString(), out float valor))
                    {
                        total += valor;
                    }
                }

                // 🔹 Redondear hacia abajo el total antes de mostrar
                total = (float)Math.Floor(total); // Trunca el valor hacia abajo

                lblTotalDoctor.Text = $"Total ingresos: Bs. {total:N2}";
            }
            else
            {
                lblTotalDoctor.Text = "Columna 'Monto Total' no disponible o sin datos.";
            }
        }



        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();

            // Obtener datos filtrados
            DataTable dtFiltrado = PacienteLogica.Instancia.ObtenerPacientesPorNombreApellido(nombre, apellido);

            // Mostrar los resultados en el DataGridView
            dgvOrina.DataSource = dtFiltrado;

            // Mostrar mensaje si no hay resultados
            if (dtFiltrado.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para el filtro aplicado.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Actualizar el total de "Monto Total"
            ActualizarTotalMonto();
        }
    }
}


