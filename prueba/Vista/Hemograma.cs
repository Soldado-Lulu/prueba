using Laboratorio.Vista;
using prueba.Logica;
using prueba.Logica_Sevicio;
using prueba.Modelo;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba.Vista
{
    public partial class Hemograma : Form
    {
        private int idPaciente;
        private PacienteM pacienteActivo;

        public Hemograma()
        {
            InitializeComponent();
            idPaciente = PacienteActivo.IdPaciente;
            LlenarCampos(idPaciente);
        }

        public Hemograma(int id)
        {
            InitializeComponent();
            idPaciente = id;
            LlenarCampos(idPaciente);
        }

        private void Hemograma_Load(object sender, EventArgs e)
        {
       
            dtpFecha.Value = DateTime.Now;

            // 🟢 Reforzamos: Si viene de nuevo paciente, tomamos de PacienteActivo
            if (idPaciente == 0)
            {
                idPaciente = PacienteActivo.IdPaciente;
            }

            pacienteActivo = PacienteLogica.Instancia.BuscarPorId(idPaciente);

            if (pacienteActivo != null)
            {
                panel1.Visible = true;
                lblNombreCompleto.Visible = true;
                lblEdad.Visible = true;
                lblMedico.Visible = true;

                lblNombreCompleto.Text = $"{pacienteActivo.Nombre} {pacienteActivo.Apellido}";
                lblEdad.Text = pacienteActivo.Edad;
                lblMedico.Text = pacienteActivo.Medico;
            }
            else
            {
                MessageBox.Show("No se encontró el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        



        public void LlenarCampos(int idPaciente)
        {
            // Llamar al método que obtiene los datos del examen Hemograma
            HemogramaM examen = HemogramaLogica.Instancia.ObtenerExamenPorPaciente(idPaciente);

            // Verificar si se encontraron datos
            if (examen != null)
            {
                // Llenar los TextBoxes con los datos del examen
                txtEritrocitos.Text = examen.Eritrocitos;
                txtLeucocitos.Text = examen.Leucocitos;
                txtHemoglobina.Text = examen.Hemoglobina;
                txtHematocritos.Text = examen.Hematocrito;
                txtPlaquetas.Text = examen.Plaquetas;
                textMielocitos.Text = examen.Mielocitos;
                textMetamielocitos.Text = examen.Melamielocitos;
                textCayados.Text = examen.Cayados;
                textSegmentados.Text = examen.Segmentados;
                textLinfocitos.Text = examen.Linfocitos;
                textMonocitos.Text = examen.Monocitos;
                textEosinofilos.Text = examen.Eosinofilos;
                textBasofilos.Text = examen.Basofilos;
                textVES1.Text = examen.VES1;
                textVES2.Text = examen.VES2;
                textIK.Text = examen.Ik;
                textGrupoSanguineo.Text = examen.GrupoSanguineo;
                textFactorRh.Text = examen.Factor;
                txtTiempoSangria.Text = examen.TiempoSangria;
                txtTiempoCoagulacion.Text = examen.TiempoCoagulacion;
                txtTiempoProtrombina.Text = examen.TiempoProtrombina;
                txtPorcentajeActividad.Text = examen.PorcentajeActividad;
                txtAPTT.Text = examen.Aptt;
                txtSerieRoja.Text = examen.SerieRoja;
                txtSerieBlanca.Text = examen.SerieBlanca;
            }
            else
            {
                MessageBox.Show("No se encontraron datos para el examen de hemograma.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear el objeto HemogramaM con los datos del formulario
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

                // Obtener el ID del paciente activo (suponiendo que pacienteActivo no es null)
                int idPaciente = pacienteActivo?.IdPaciente ?? 0;

                // Crear el objeto PacienteM con los datos del paciente (estos deben venir de algún lugar)
                PacienteM paciente = new PacienteM()
                {
                    IdPaciente = idPaciente,
                    Nombre = lblNombreCompleto.Text,  // Asegúrate de tener un control de nombre
                    Apellido = lblMedico.Text,  // Asegúrate de tener un control de apellido
                    Telefono = lblEdad.Text,  // Asegúrate de tener un control de teléfono
                };

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
                    // Guardar el paciente y hemograma en la base de datos
                    guardado = HemogramaLogica.Instancia.GuardarNuevoPaciente(objeto, paciente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en Guardar:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                     "Error en Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si la operación fue exitosa
                if (guardado)
                {
                    MessageBox.Show("Examen de Hemograma y Paciente guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al guardar el examen de Hemograma o el paciente. No se insertaron los datos correctamente.\n\n{datosMensaje}",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                 "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


            CapturarPanel(PanelCap); // Reemplaza 'panel1' con el nombre de tu panel.

            // Configurar el documento de impresión
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Mostrar el cuadro de diálogo de impresión
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private Bitmap panelBitmap;
        private void CapturarPanel(Panel panel)
        {
            // Crear un Bitmap con el tamaño del panel
            panelBitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panel.Width, panel.Height));
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Dibujar el contenido del panel capturado en la página de impresión
            if (panelBitmap != null)
            {
                e.Graphics.DrawImage(panelBitmap, new Point(0, 0));
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

        private void btnNuevoPaciente_Click_1(object sender, EventArgs e)
        {
            PacienteActivo.Limpiar(); // ✔️ Limpiar datos en memoria

            RegistroPaciente form = new RegistroPaciente();
            form.Show();
            this.Close(); // ✔️ Cierra el formulario actual para liberar recursos
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Reporte formQuimica = new Reporte();
            formQuimica.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
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

            bool actualizado = HemogramaLogica.Instancia.ActualizarExamen(objeto, idPaciente);

            if (actualizado)
                MessageBox.Show("Examen actualizado correctamente.");
            else
                MessageBox.Show("No se pudo actualizar el examen.");
        }

    }
}

