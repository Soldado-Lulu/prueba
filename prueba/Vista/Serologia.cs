﻿using Laboratorio.Vista;
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
    public partial class Serologia : Form
    {
        private PacienteM pacienteActivo;
        private int idPaciente;
        public Serologia(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
        }
        private void CargarDatos()
        {
            // Aquí consultas la base de datos y llenas los campos con los datos del paciente
        }
        public Serologia()
        {
            InitializeComponent();
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                SerologiaM objeto = new SerologiaM()
                {
                    Reaccion = txtReaccion.Text,
                    Campo11 = txtCampo11.Text,
                    Campo12 = c12.Text,
                    Campo13 = c13.Text,
                    Campo14 = c14.Text,
                    Campo15 = c15.Text,
                    Campo21 = c21.Text,
                    Campo22 = c22.Text,
                    Campo23 = c23.Text,
                    Campo24 = c24.Text,
                    Campo25 = c25.Text,
                    Campo31 = c31.Text,
                    Campo32 = c32.Text,
                    Campo33 = c33.Text,
                    Campo34 = c34.Text,
                    Campo35 = c35.Text,
                    Campo41 = c41.Text,
                    Campo42 = c42.Text,
                    Campo43 = c43.Text,
                    Campo44 = c44.Text,
                    Campo45 = c45.Text,
                    Observaciones = txtObservaciones.Text
                };

                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores si pacienteActivo es null

                // Mostrar los datos antes de guardar
                string datosMensaje = $"Intentando guardar Serología:\n" +
                                      $"Reacción: {objeto.Reaccion}\n" +
                                      $"Campo11: {objeto.Campo11}\n" +
                                      $"Campo12: {objeto.Campo12}\n" +
                                      $"Campo13: {objeto.Campo13}\n" +
                                      $"Campo14: {objeto.Campo14}\n" +
                                      $"Campo15: {objeto.Campo15}\n" +
                                      $"Campo21: {objeto.Campo21}\n" +
                                      $"Campo22: {objeto.Campo22}\n" +
                                      $"Campo23: {objeto.Campo23}\n" +
                                      $"Campo24: {objeto.Campo24}\n" +
                                      $"Campo25: {objeto.Campo25}\n" +
                                      $"Campo31: {objeto.Campo31}\n" +
                                      $"Campo32: {objeto.Campo32}\n" +
                                      $"Campo33: {objeto.Campo33}\n" +
                                      $"Campo34: {objeto.Campo34}\n" +
                                      $"Campo35: {objeto.Campo35}\n" +
                                      $"Campo41: {objeto.Campo41}\n" +
                                      $"Campo42: {objeto.Campo42}\n" +
                                      $"Campo43: {objeto.Campo43}\n" +
                                      $"Campo44: {objeto.Campo44}\n" +
                                      $"Campo45: {objeto.Campo45}\n" +
                                      $"Observaciones: {objeto.Observaciones}\n" +
                                      $"IdPaciente: {idPaciente}";

                MessageBox.Show(datosMensaje, "Datos antes de guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool guardado = false;

                try
                {
                    guardado = SerologiaLogica.Instancia.GuardarExamen(objeto, idPaciente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en Guardar:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                    "Error en Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (guardado)
                {
                    MessageBox.Show("Examen de Serología guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al guardar el examen de Serología. No se insertaron los datos correctamente.\n\n{datosMensaje}",
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
        private void Serologia_Load(object sender, EventArgs e)
        {

            dtpFecha.Value = DateTime.Now;
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
            Hemograma formQuimica = new Hemograma();
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

        private void btnNuevoPAciente_Click_1(object sender, EventArgs e)
        {
            RegistroPaciente formQuimica = new RegistroPaciente();
            formQuimica.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reporte formQuimica = new Reporte();
            formQuimica.Show();
            this.Hide();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblNombreCompleto_Click(object sender, EventArgs e)
        {

        }

        private void lblMedico_Click(object sender, EventArgs e)
        {

        }

        private void label65_Click(object sender, EventArgs e)
        {

        }
    }
}
