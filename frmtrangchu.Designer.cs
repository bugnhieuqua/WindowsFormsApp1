namespace WindowsFormsApp1
{
    partial class frmtrangchu
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
            this.btt1 = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgbang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgbang)).BeginInit();
            this.SuspendLayout();
            // 
            // btt1
            // 
            this.btt1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btt1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btt1.ForeColor = System.Drawing.Color.Maroon;
            this.btt1.Location = new System.Drawing.Point(247, 163);
            this.btt1.Margin = new System.Windows.Forms.Padding(2);
            this.btt1.Name = "btt1";
            this.btt1.Size = new System.Drawing.Size(119, 40);
            this.btt1.TabIndex = 0;
            this.btt1.Text = "START";
            this.btt1.UseVisualStyleBackColor = false;
            this.btt1.Click += new System.EventHandler(this.btt1_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbl1.Font = new System.Drawing.Font("Tempus Sans ITC", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl1.Location = new System.Drawing.Point(85, 71);
            this.lbl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(465, 62);
            this.lbl1.TabIndex = 2;
            this.lbl1.Text = "COW OF SURVIVAL";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // dgbang
            // 
            this.dgbang.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgbang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbang.Location = new System.Drawing.Point(12, 220);
            this.dgbang.Name = "dgbang";
            this.dgbang.Size = new System.Drawing.Size(576, 134);
            this.dgbang.TabIndex = 3;
            // 
            // frmtrangchu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources._325608924ec64cec5fe900e836bbcea51;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.dgbang);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btt1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmtrangchu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmtrangchu";
            this.Load += new System.EventHandler(this.frmtrangchu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgbang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btt1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgbang;
    }
}
