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
    public partial class Micro : Form
    {
        private PacienteM pacienteActivo;
        public Micro()
        {
            InitializeComponent();
        }
        private int idPaciente;
        public Micro(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
        }
        private void CargarDatos()
        {
            // Aquí consultas la base de datos y llenas los campos con los datos del paciente
        }
        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                MicroM objeto = new MicroM()
                {
                    Muestra = txtMuestra.Text,
                    Gram = txtGram.Text,
                    M1 = txtM1.Text,
                    M2 = txtM2.Text,
                    M3 = txtM3.Text,
                    Cultivo = txtCultivo.Text,
                    Colonia = txtColonia.Text,
                    Identificacion = txtIdentificacion.Text,
                    Sensible = txtSensibles.Text,
                    Resistencia = txtResistentes.Text
                };

                int idPaciente = pacienteActivo?.IdPaciente ?? 0; // Previene errores si pacienteActivo es null

                // Mostrar los datos antes de guardar
                string datosMensaje = $"Intentando guardar Microbiología:\n" +
                                      $"Muestra: {objeto.Muestra}\n" +
                                      $"Gram: {objeto.Gram}\n" +
                                      $"M1: {objeto.M1}\n" +
                                      $"M2: {objeto.M2}\n" +
                                      $"M3: {objeto.M3}\n" +
                                      $"Cultivo: {objeto.Cultivo}\n" +
                                      $"Colonia: {objeto.Colonia}\n" +
                                      $"Identificación: {objeto.Identificacion}\n" +
                                      $"Sensible: {objeto.Sensible}\n" +
                                      $"Resistencia: {objeto.Resistencia}\n" +
                                      $"IdPaciente: {idPaciente}";

                MessageBox.Show(datosMensaje, "Datos antes de guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool guardado = false;

                try
                {
                    guardado = MicroLogica.Instancia.GuardarExamen(objeto, idPaciente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en Guardar:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}",
                                    "Error en Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (guardado)
                {
                    MessageBox.Show("Examen de Microbiología guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al guardar el examen de Microbiología. No se insertaron los datos correctamente.\n\n{datosMensaje}",
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

        private void Micro_Load(object sender, EventArgs e)
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
            Serologia formQuimica = new Serologia();
            formQuimica.Show();
            this.Hide();
        }

        private void btnMicro_Click(object sender, EventArgs e)
        {
            Hemograma formQuimica = new Hemograma();
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

        private void btnNuevopaciente_Click_1(object sender, EventArgs e)
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
    }
}
