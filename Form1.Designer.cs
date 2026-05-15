namespace WindowsFormsApp1
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.btt1 = new System.Windows.Forms.Button();
            this.btt2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Matura MT Script Capitals", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(232, 195);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 28);
            this.textBox1.TabIndex = 1;
            this.textBox1.Tag = "";
            this.textBox1.Text = "Users";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("PMingLiU-ExtB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(232, 244);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(228, 28);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Pass";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl3.Font = new System.Drawing.Font("Viner Hand ITC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.ForeColor = System.Drawing.SystemColors.MenuText;
            this.lbl3.Image = global::WindowsFormsApp1.Properties.Resources._325608924ec64cec5fe900e836bbcea51;
            this.lbl3.Location = new System.Drawing.Point(224, 108);
            this.lbl3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(253, 85);
            this.lbl3.TabIndex = 0;
            this.lbl3.Text = "       Hello!\r\nLOGIN, please!!";
            this.lbl3.Click += new System.EventHandler(this.lbl1_Click);
            // 
            // btt1
            // 
            this.btt1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btt1.Location = new System.Drawing.Point(232, 301);
            this.btt1.Margin = new System.Windows.Forms.Padding(2);
            this.btt1.Name = "btt1";
            this.btt1.Size = new System.Drawing.Size(86, 31);
            this.btt1.TabIndex = 2;
            this.btt1.Text = "LOGIN";
            this.btt1.UseVisualStyleBackColor = true;
            this.btt1.Click += new System.EventHandler(this.btt1_Click);
            // 
            // btt2
            // 
            this.btt2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btt2.Location = new System.Drawing.Point(373, 301);
            this.btt2.Margin = new System.Windows.Forms.Padding(2);
            this.btt2.Name = "btt2";
            this.btt2.Size = new System.Drawing.Size(86, 31);
            this.btt2.TabIndex = 2;
            this.btt2.Text = "FORGET";
            this.btt2.UseVisualStyleBackColor = true;
            this.btt2.Click += new System.EventHandler(this.btt2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources._709c7e59556dc7d9f02ee9e54093ee6a2;
            this.pictureBox1.Location = new System.Drawing.Point(302, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources._325608924ec64cec5fe900e836bbcea5;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(716, 366);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btt2);
            this.Controls.Add(this.btt1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbl3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Button btt1;
        private System.Windows.Forms.Button btt2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

