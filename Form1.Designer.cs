namespace Moorhuhn
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblZeit = new Label();
            lblPunktzahl = new Label();
            SuspendLayout();
            // 
            // lblZeit
            // 
            lblZeit.AutoSize = true;
            lblZeit.Location = new Point(715, 12);
            lblZeit.Name = "lblZeit";
            lblZeit.Size = new Size(27, 15);
            lblZeit.TabIndex = 0;
            lblZeit.Text = "Zeit";
            // 
            // lblPunktzahl
            // 
            lblPunktzahl.AutoSize = true;
            lblPunktzahl.Location = new Point(25, 16);
            lblPunktzahl.Name = "lblPunktzahl";
            lblPunktzahl.Size = new Size(38, 15);
            lblPunktzahl.TabIndex = 1;
            lblPunktzahl.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblPunktzahl);
            Controls.Add(lblZeit);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblZeit;
        private Label lblPunktzahl;
    }
}