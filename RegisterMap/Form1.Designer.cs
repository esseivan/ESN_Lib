namespace RegisterMap
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
            this.textboxWatermark1 = new EsseivaN.Controls.TextboxWatermark();
            this.textboxWatermark2 = new EsseivaN.Controls.TextboxWatermark();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textboxWatermark1
            // 
            this.textboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.Location = new System.Drawing.Point(12, 12);
            this.textboxWatermark1.Name = "textboxWatermark1";
            this.textboxWatermark1.Size = new System.Drawing.Size(100, 20);
            this.textboxWatermark1.TabIndex = 0;
            this.textboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.WatermarkText = "Type here...";
            // 
            // textboxWatermark2
            // 
            this.textboxWatermark2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.Location = new System.Drawing.Point(12, 38);
            this.textboxWatermark2.Name = "textboxWatermark2";
            this.textboxWatermark2.Size = new System.Drawing.Size(100, 20);
            this.textboxWatermark2.TabIndex = 0;
            this.textboxWatermark2.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.WatermarkText = "Type here...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textboxWatermark2);
            this.Controls.Add(this.textboxWatermark1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EsseivaN.Controls.TextboxWatermark textboxWatermark1;
        private EsseivaN.Controls.TextboxWatermark textboxWatermark2;
        private System.Windows.Forms.Button button1;
    }
}

