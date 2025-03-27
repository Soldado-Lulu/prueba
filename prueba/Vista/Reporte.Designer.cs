namespace prueba.Vista
{
    partial class Reporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvOrina = new System.Windows.Forms.DataGridView();
            this.cmbMedico = new System.Windows.Forms.ComboBox();
            this.lblTotalDoctor = new System.Windows.Forms.Label();
            this.btnInicio = new System.Windows.Forms.Button();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrina)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrina
            // 
            this.dgvOrina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrina.Location = new System.Drawing.Point(32, 132);
            this.dgvOrina.Name = "dgvOrina";
            this.dgvOrina.RowHeadersWidth = 51;
            this.dgvOrina.RowTemplate.Height = 24;
            this.dgvOrina.Size = new System.Drawing.Size(1103, 585);
            this.dgvOrina.TabIndex = 0;
            this.dgvOrina.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrina_CellContentClick);
            // 
            // cmbMedico
            // 
            this.cmbMedico.FormattingEnabled = true;
            this.cmbMedico.Location = new System.Drawing.Point(43, 55);
            this.cmbMedico.Name = "cmbMedico";
            this.cmbMedico.Size = new System.Drawing.Size(121, 24);
            this.cmbMedico.TabIndex = 1;
            this.cmbMedico.SelectedIndexChanged += new System.EventHandler(this.cmbMedico_SelectedIndexChanged);
            // 
            // lblTotalDoctor
            // 
            this.lblTotalDoctor.AutoSize = true;
            this.lblTotalDoctor.Location = new System.Drawing.Point(40, 96);
            this.lblTotalDoctor.Name = "lblTotalDoctor";
            this.lblTotalDoctor.Size = new System.Drawing.Size(0, 16);
            this.lblTotalDoctor.TabIndex = 2;
            this.lblTotalDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInicio
            // 
            this.btnInicio.Location = new System.Drawing.Point(1211, 132);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(219, 55);
            this.btnInicio.TabIndex = 3;
            this.btnInicio.Text = "Atras";
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(196, 56);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(262, 22);
            this.dtpDesde.TabIndex = 4;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(480, 57);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(261, 22);
            this.dtpHasta.TabIndex = 5;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(747, 56);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(173, 23);
            this.btnFiltrar.TabIndex = 6;
            this.btnFiltrar.Text = "Clacular Por Fechas";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 896);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.lblTotalDoctor);
            this.Controls.Add(this.cmbMedico);
            this.Controls.Add(this.dgvOrina);
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrina;
        private System.Windows.Forms.ComboBox cmbMedico;
        private System.Windows.Forms.Label lblTotalDoctor;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnFiltrar;
    }
}