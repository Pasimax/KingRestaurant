namespace Final_Resturant
{
    partial class invoice_view
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Restaurant_slip = new Final_Resturant.Restaurant_slip();
            this.SlipBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SlipTableAdapter = new Final_Resturant.Restaurant_slipTableAdapters.SlipTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Restaurant_slip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SlipBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Restaurant_slip";
            reportDataSource1.Value = this.SlipBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Final_Resturant.Report_slip.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(483, 579);
            this.reportViewer1.TabIndex = 0;
            // 
            // Restaurant_slip
            // 
            this.Restaurant_slip.DataSetName = "Restaurant_slip";
            this.Restaurant_slip.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SlipBindingSource
            // 
            this.SlipBindingSource.DataMember = "Slip";
            this.SlipBindingSource.DataSource = this.Restaurant_slip;
            // 
            // SlipTableAdapter
            // 
            this.SlipTableAdapter.ClearBeforeFill = true;
            // 
            // invoice_view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 579);
            this.Controls.Add(this.reportViewer1);
            this.Name = "invoice_view";
            this.Text = "invoice_view";
            this.Load += new System.EventHandler(this.invoice_view_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Restaurant_slip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SlipBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SlipBindingSource;
        private Restaurant_slip Restaurant_slip;
        private Restaurant_slipTableAdapters.SlipTableAdapter SlipTableAdapter;
    }
}