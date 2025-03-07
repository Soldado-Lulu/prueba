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
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrina)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrina
            // 
            this.dgvOrina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrina.Location = new System.Drawing.Point(186, 67);
            this.dgvOrina.Name = "dgvOrina";
            this.dgvOrina.RowHeadersWidth = 51;
            this.dgvOrina.RowTemplate.Height = 24;
            this.dgvOrina.Size = new System.Drawing.Size(1114, 535);
            this.dgvOrina.TabIndex = 0;
            this.dgvOrina.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrina_CellContentClick);
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 896);
            this.Controls.Add(this.dgvOrina);
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.Reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrina)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrina;
    }
}