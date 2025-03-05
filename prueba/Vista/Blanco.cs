using prueba.Logica;
using prueba.Logica_Sevicio;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            LlenarDataGridView();

        }
        private void LlenarDataGridView()
        {
            dgvOrina.DataSource = PacienteLogica.Instancia.ObtenerPacientesConExamenes();
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
            try
            {
                BlancoM objeto = new BlancoM()
                {
                    Muestra = txtMuestra.Text,
                    Examen = txtExamen.Text,
                    Datos = txtDatos.Text,
                    Otros = txtOtros.Text,
                };

                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores de null

                // Mostrar los datos antes de guardar
                string datosMensaje = $"Intentando guardar:\n" +
                                      $"Muestra: {objeto.Muestra}\n" +
                                      $"Examen: {objeto.Examen}\n" +
                                      $"Datos: {objeto.Datos}\n" +
                                      $"Otros: {objeto.Otros}\n" +
                                      $"IdPaciente: {idPaciente}";
                MessageBox.Show(datosMensaje, "Datos antes de guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool guardado = false;

                try
                {
                    guardado = BlancoLogica.Instancia.GuardarExamen(objeto, idPaciente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en GuardarExamen:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                    "Error en Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (guardado)
                {
                    MessageBox.Show("Examen de Blanco guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al guardar el examen de Blanco. No se insertaron los datos correctamente.\n\n{datosMensaje}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
