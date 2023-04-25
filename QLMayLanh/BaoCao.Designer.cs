namespace QLMayLanh
{
    partial class BaoCao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaoCao));
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btn_Quaylai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(1, 4);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(973, 389);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // btn_Quaylai
            // 
            this.btn_Quaylai.Image = ((System.Drawing.Image)(resources.GetObject("btn_Quaylai.Image")));
            this.btn_Quaylai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Quaylai.Location = new System.Drawing.Point(38, 403);
            this.btn_Quaylai.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Quaylai.Name = "btn_Quaylai";
            this.btn_Quaylai.Size = new System.Drawing.Size(89, 38);
            this.btn_Quaylai.TabIndex = 1;
            this.btn_Quaylai.Text = "Quay Lại";
            this.btn_Quaylai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Quaylai.UseVisualStyleBackColor = true;
            this.btn_Quaylai.Click += new System.EventHandler(this.btn_Quaylai_Click);
            // 
            // BaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 452);
            this.Controls.Add(this.btn_Quaylai);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "BaoCao";
            this.Text = "BaoCao";
            this.Load += new System.EventHandler(this.BaoCao_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Button btn_Quaylai;
    }
}