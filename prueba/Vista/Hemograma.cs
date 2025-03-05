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
    public partial class Hemograma : Form
    {
        private PacienteM pacienteActivo;
        public Hemograma()
        {
            InitializeComponent();
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                HemogramaM objeto = new HemogramaM()
                {
                    Eritrocitos = txtEritrocitos.Text,
                    Leucocitos = txtLeucocitos.Text,
                    Hemoglobina = txtHemoglobina.Text,
                    Hematocrito = txtHematocritos.Text,
                    Plaquetas = txtPlaquetas.Text,
                    Mielocitos = textMielocitos.Text,
                    Melamielocitos = textMetamielocitos.Text,
                    Cayados = textCayados.Text,
                    Segmentados = textSegmentados.Text,
                    Linfocitos = textLinfocitos.Text,
                    Monocitos = textMonocitos.Text,
                    Eosinofilos = textEosinofilos.Text,
                    Basofilos = textBasofilos.Text,
                    VES1 = textVES1.Text,
                    VES2 = textVES2.Text,
                    Ik = textIK.Text,
                    GrupoSanguineo = textGrupoSanguineo.Text,
                    Factor = textFactorRh.Text,
                    TiempoSangria = txtTiempoSangria.Text,
                    TiempoCoagulacion = txtTiempoCoagulacion.Text,
                    TiempoProtrombina = txtTiempoProtrombina.Text,
                    PorcentajeActividad = txtPorcentajeActividad.Text,
                    Aptt = txtAPTT.Text,
                    SerieRoja = txtSerieRoja.Text,
                    SerieBlanca = txtSerieBlanca.Text
                };

                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores si pacienteActivo es null

                // Mostrar los datos antes de guardar
                string datosMensaje = $"Intentando guardar Hemograma:\n" +
                                      $"Eritrocitos: {objeto.Eritrocitos}\n" +
                                      $"Leucocitos: {objeto.Leucocitos}\n" +
                                      $"Hemoglobina: {objeto.Hemoglobina}\n" +
                                      $"Hematocrito: {objeto.Hematocrito}\n" +
                                      $"Plaquetas: {objeto.Plaquetas}\n" +
                                      $"Mielocitos: {objeto.Mielocitos}\n" +
                                      $"Melamielocitos: {objeto.Melamielocitos}\n" +
                                      $"Cayados: {objeto.Cayados}\n" +
                                      $"Segmentados: {objeto.Segmentados}\n" +
                                      $"Linfocitos: {objeto.Linfocitos}\n" +
                                      $"Monocitos: {objeto.Monocitos}\n" +
                                      $"Eosinofilos: {objeto.Eosinofilos}\n" +
                                      $"Basofilos: {objeto.Basofilos}\n" +
                                      $"VES1: {objeto.VES1}\n" +
                                      $"VES2: {objeto.VES2}\n" +
                                      $"Ik: {objeto.Ik}\n" +
                                      $"Grupo Sanguíneo: {objeto.GrupoSanguineo}\n" +
                                      $"Factor: {objeto.Factor}\n" +
                                      $"Tiempo de Sangría: {objeto.TiempoSangria}\n" +
                                      $"Tiempo de Coagulación: {objeto.TiempoCoagulacion}\n" +
                                      $"Tiempo de Protrombina: {objeto.TiempoProtrombina}\n" +
                                      $"Porcentaje de Actividad: {objeto.PorcentajeActividad}\n" +
                                      $"APTT: {objeto.Aptt}\n" +
                                      $"Serie Roja: {objeto.SerieRoja}\n" +
                                      $"Serie Blanca: {objeto.SerieBlanca}\n" +
                                      $"IdPaciente: {idPaciente}";

                MessageBox.Show(datosMensaje, "Datos antes de guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool guardado = false;

                try
                {
                    guardado = HemogramaLogica.Instancia.GuardarExamen(objeto, idPaciente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en Guardar:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                    "Error en Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (guardado)
                {
                    MessageBox.Show("Examen de Hemograma guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al guardar el examen de Hemograma. No se insertaron los datos correctamente.\n\n{datosMensaje}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Hemograma_Load(object sender, EventArgs e)
        {
            // Obtener el último paciente registrado
            //PacienteM paciente = PacienteLogica.Instancia.ObtenerUltimoPaciente();
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

        private void btnHemograma_Click(object sender, EventArgs e)
        {
            Quimica formQuimica = new Quimica();
            formQuimica.Show();  // Abre el formulario de Química
            this.Hide();  // Oculta el formulario actual
        }

        private void btnOrina_Click(object sender, EventArgs e)
        {
            Orina formQuimica = new Orina();
            formQuimica.Show();
            this.Hide();
        }

        private void btnCopros_Click(object sender, EventArgs e)
        {
            Copros formQuimica = new Copros();
            formQuimica.Show();
            this.Hide();
        }

        private void btnHCG_Click(object sender, EventArgs e)
        {
            HCG formQuimica = new HCG();
            formQuimica.Show();
            this.Hide();
        }

        private void btnSerologia_Click(object sender, EventArgs e)
        {
            Serologia formQuimica = new Serologia();
            formQuimica.Show();
            this.Hide();
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            Micro formQuimica = new Micro();
            formQuimica.Show();
            this.Hide();
        }

        private void btnBlanco_Click(object sender, EventArgs e)
        {
            Blanco formQuimica = new Blanco();
            formQuimica.Show();
            this.Hide();
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            Sobre formQuimica = new Sobre();
            formQuimica.Show();
            this.Hide();
        }

        private void btnVarios_Click(object sender, EventArgs e)
        {
            Varios formQuimica = new Varios();
            formQuimica.Show();
            this.Hide();
        }
    }
}
