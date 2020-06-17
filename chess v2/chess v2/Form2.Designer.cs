namespace chess_v2
{
    partial class Form2
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
            this.kraljica = new System.Windows.Forms.Button();
            this.top = new System.Windows.Forms.Button();
            this.konj = new System.Windows.Forms.Button();
            this.lovac = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // kraljica
            // 
            this.kraljica.Location = new System.Drawing.Point(54, 72);
            this.kraljica.Name = "kraljica";
            this.kraljica.Size = new System.Drawing.Size(95, 85);
            this.kraljica.TabIndex = 0;
            this.kraljica.Text = "Kraljica";
            this.kraljica.UseVisualStyleBackColor = true;
            this.kraljica.Click += new System.EventHandler(this.kraljica_Click);
            // 
            // top
            // 
            this.top.Location = new System.Drawing.Point(183, 72);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(95, 85);
            this.top.TabIndex = 1;
            this.top.Text = "top";
            this.top.UseVisualStyleBackColor = true;
            this.top.Click += new System.EventHandler(this.top_Click);
            // 
            // konj
            // 
            this.konj.Location = new System.Drawing.Point(318, 72);
            this.konj.Name = "konj";
            this.konj.Size = new System.Drawing.Size(95, 85);
            this.konj.TabIndex = 2;
            this.konj.Text = "konj";
            this.konj.UseVisualStyleBackColor = true;
            this.konj.Click += new System.EventHandler(this.konj_Click);
            // 
            // lovac
            // 
            this.lovac.Location = new System.Drawing.Point(449, 72);
            this.lovac.Name = "lovac";
            this.lovac.Size = new System.Drawing.Size(95, 85);
            this.lovac.TabIndex = 3;
            this.lovac.Text = "lovac";
            this.lovac.UseVisualStyleBackColor = true;
            this.lovac.Click += new System.EventHandler(this.lovac_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 227);
            this.Controls.Add(this.lovac);
            this.Controls.Add(this.konj);
            this.Controls.Add(this.top);
            this.Controls.Add(this.kraljica);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button kraljica;
        private System.Windows.Forms.Button top;
        private System.Windows.Forms.Button konj;
        private System.Windows.Forms.Button lovac;
    }
}