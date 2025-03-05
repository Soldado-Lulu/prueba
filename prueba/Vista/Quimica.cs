using prueba.Logica;
using prueba.Logica_Sevicio;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba.Vista
{
    public partial class Quimica : Form
    {
        private PacienteM pacienteActivo;

        public Quimica()
        {
            InitializeComponent();
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un objeto QuimicaM con los valores de los controles de texto
                QuimicaM objeto = new QuimicaM()
                {
                    Glucosa = txtGlucosa.Text,
                    Urea = txtUrea.Text,
                    Creatina = txtCreatinina.Text,
                    BUN = txtBUN.Text,
                    Urico = txtUrico.Text,
                    Colesterol = txtColesterol.Text,
                    HDL = txtHDL.Text,
                    LDL = txtLDL.Text,
                    Triglicerido = txtTrigliceridos.Text,
                    Bilirrubina = txtBilirrubina.Text,
                    Directa = txtDirecta.Text,
                    Indirecta = txtIndirecta.Text,
                    Total = txtTotal.Text,
                    GOT = txtGOT.Text,
                    GPT = txtGPT.Text,
                    FosfatasaAlcalina = txtFosfatasaAlcalina.Text,
                    Amilasa = txtAmilasa.Text,
                    Proteina = txtProteina.Text,
                    Albumina = txtAlbumina.Text,
                    Globulina = txtglobulina.Text,
                    Relacion = txtRelacion.Text,
                    FosfatasaAcida = txtFosfatasaAcida.Text,
                    FosfatasaAcidaProstatica = txtFosfatasaAcidaProstatica.Text,
                    CKMB = txtCKMB.Text,
                    CPK = txtCPK.Text,
                    Hemoglobina = txtHemoglobina.Text,
                    Sodio = txtSodio.Text,
                    Potasio = txtPotasio.Text,
                    Cloro = txtCloro.Text,
                    Calcio = txtCalcio.Text
                };

                // Obtener el idPaciente del pacienteActivo
                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores si pacienteActivo es null

                // Mostrar los datos antes de guardar
                string datosMensaje = $"Intentando guardar Química Sanguínea:\n" +
                                      $"Glucosa: {objeto.Glucosa}\n" +
                                      $"Urea: {objeto.Urea}\n" +
                                      $"Creatina: {objeto.Creatina}\n" +
                                      $"BUN: {objeto.BUN}\n" +
                                      $"Ácido Úrico: {objeto.Urico}\n" +
                                      $"Colesterol: {objeto.Colesterol}\n" +
                                      $"HDL: {objeto.HDL}\n" +
                                      $"LDL: {objeto.LDL}\n" +
                                      $"Triglicéridos: {objeto.Triglicerido}\n" +
                                      $"Bilirrubina: {objeto.Bilirrubina}\n" +
                                      $"Directa: {objeto.Directa}\n" +
                                      $"Indirecta: {objeto.Indirecta}\n" +
                                      $"Total: {objeto.Total}\n" +
                                      $"GOT: {objeto.GOT}\n" +
                                      $"GPT: {objeto.GPT}\n" +
                                      $"Fosfatasa Alcalina: {objeto.FosfatasaAlcalina}\n" +
                                      $"Amilasa: {objeto.Amilasa}\n" +
                                      $"Proteína: {objeto.Proteina}\n" +
                                      $"Albúmina: {objeto.Albumina}\n" +
                                      $"Globulina: {objeto.Globulina}\n" +
                                      $"Relación: {objeto.Relacion}\n" +
                                      $"Fosfatasa Ácida: {objeto.FosfatasaAcida}\n" +
                                      $"Fosfatasa Ácida Prostática: {objeto.FosfatasaAcidaProstatica}\n" +
                                      $"CKMB: {objeto.CKMB}\n" +
                                      $"CPK: {objeto.CPK}\n" +
                                      $"Hemoglobina: {objeto.Hemoglobina}\n" +
                                      $"Sodio: {objeto.Sodio}\n" +
                                      $"Potasio: {objeto.Potasio}\n" +
                                      $"Cloro: {objeto.Cloro}\n" +
                                      $"Calcio: {objeto.Calcio}\n" +
                                      $"IdPaciente: {idPaciente}";

                // Mostrar los datos en un mensaje
                MessageBox.Show(datosMensaje, "Datos antes de guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool guardado = false;

                try
                {
                    // Intentar guardar los datos
                    guardado = QuimicaLogica.Instancia.GuardarExamen(objeto, idPaciente);
                }
                catch (SQLiteException ex)
                {
                    // Capturar errores de la base de datos SQLite
                    MessageBox.Show($"Error en Guardado (SQLite):\n{ex.Message}\nCódigo de error: {ex.ErrorCode}\nDetalles: {ex.StackTrace}",
                                     "Error en Guardado SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (InvalidOperationException ex)
                {
                    // Capturar errores de operación inválida en la base de datos
                    MessageBox.Show($"Error en Guardado (Operación Inválida):\n{ex.Message}\nDetalles: {ex.StackTrace}",
                                     "Error en Guardado Operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    // Capturar cualquier otro error inesperado
                    MessageBox.Show($"Error inesperado en el fragmento de código:\n" +
                                     $"Método: btnNuevoPaciente_Click\n" +
                                     $"Mensaje: {ex.Message}\nDetalles: {ex.StackTrace}",
                                     "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si el guardado fue exitoso
                if (guardado)
                {
                    MessageBox.Show("Examen de Química Sanguínea guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Mostrar mensaje de error si no se insertaron los datos correctamente
                    MessageBox.Show($"Error al guardar el examen de Química Sanguínea. No se insertaron los datos correctamente.\n\n{datosMensaje}",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier error inesperado fuera del bloque principal
                MessageBox.Show($"Ocurrió un error inesperado en la ejecución:\n" +
                                 $"Método: btnNuevoPaciente_Click\n" +
                                 $"Mensaje: {ex.Message}\nDetalles: {ex.StackTrace}",
                                 "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Quimica_Load(object sender, EventArgs e)
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
            Hemograma formQuimica = new Hemograma();
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

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
