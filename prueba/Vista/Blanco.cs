﻿using Laboratorio.Vista;
using prueba.Logica;
using prueba.Logica_Sevicio;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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
           // string tipoExamen = "orina"; // Cambia esto si es necesario
         //   CargarDatosEnDataGridView(tipoExamen);

        }
        private void CargarDatosEnDataGridView(string tipoExamen)
        {
            // Llamar al método para obtener los datos del examen seleccionado
           // dgvOrina.DataSource = PacienteLogica.Instacia.ObtenerPacientesConExamenes(tipoExamen);
    
        }

        private int idPaciente;
        public Blanco(int id)
        {
            InitializeComponent();
            idPaciente = id;
            CargarDatos();
        }
        private void CargarDatos()
        {
            // Aquí consultas la base de datos y llenas los campos con los datos del paciente
        }
        private void Blanco_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;

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
            // Capturar el contenido del panel
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
            Quimica formQuimica = new Quimica();
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

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
