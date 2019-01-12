namespace tests
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
            this.button1 = new System.Windows.Forms.Button();
            this.mL1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.mL2 = new System.Windows.Forms.ComboBox();
            this.mL3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mMsg = new EsseivaN.Controls.TextboxWatermark();
            this.mTitle = new EsseivaN.Controls.TextboxWatermark();
            this.mB3 = new EsseivaN.Controls.TextboxWatermark();
            this.mB2 = new EsseivaN.Controls.TextboxWatermark();
            this.mB1 = new EsseivaN.Controls.TextboxWatermark();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.textboxWatermark3 = new EsseivaN.Controls.TextboxWatermark();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textboxWatermark2 = new EsseivaN.Controls.TextboxWatermark();
            this.textboxWatermark1 = new EsseivaN.Controls.TextboxWatermark();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Download update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mL1
            // 
            this.mL1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mL1.FormattingEnabled = true;
            this.mL1.Location = new System.Drawing.Point(145, 18);
            this.mL1.Name = "mL1";
            this.mL1.Size = new System.Drawing.Size(121, 21);
            this.mL1.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Show messagebox";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mL2
            // 
            this.mL2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mL2.FormattingEnabled = true;
            this.mL2.Location = new System.Drawing.Point(145, 71);
            this.mL2.Name = "mL2";
            this.mL2.Size = new System.Drawing.Size(121, 21);
            this.mL2.TabIndex = 15;
            // 
            // mL3
            // 
            this.mL3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mL3.FormattingEnabled = true;
            this.mL3.Location = new System.Drawing.Point(145, 124);
            this.mL3.Name = "mL3";
            this.mL3.Size = new System.Drawing.Size(121, 21);
            this.mL3.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mMsg);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mL1);
            this.groupBox1.Controls.Add(this.mTitle);
            this.groupBox1.Controls.Add(this.mL2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.mB3);
            this.groupBox1.Controls.Add(this.mL3);
            this.groupBox1.Controls.Add(this.mB2);
            this.groupBox1.Controls.Add(this.mB1);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 203);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DialogInput";
            // 
            // mMsg
            // 
            this.mMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mMsg.Location = new System.Drawing.Point(6, 19);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(121, 20);
            this.mMsg.TabIndex = 11;
            this.mMsg.Text = "Message";
            this.mMsg.TextColor = System.Drawing.SystemColors.ControlText;
            this.mMsg.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mMsg.WatermarkText = "Message";
            // 
            // mTitle
            // 
            this.mTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mTitle.Location = new System.Drawing.Point(6, 45);
            this.mTitle.Name = "mTitle";
            this.mTitle.Size = new System.Drawing.Size(121, 20);
            this.mTitle.TabIndex = 12;
            this.mTitle.Text = "Title";
            this.mTitle.TextColor = System.Drawing.SystemColors.ControlText;
            this.mTitle.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mTitle.WatermarkText = "Title";
            // 
            // mB3
            // 
            this.mB3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB3.Location = new System.Drawing.Point(145, 151);
            this.mB3.Name = "mB3";
            this.mB3.Size = new System.Drawing.Size(121, 20);
            this.mB3.TabIndex = 18;
            this.mB3.Text = "_Btn3_";
            this.mB3.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB3.WatermarkColor = System.Drawing.Color.Red;
            this.mB3.WatermarkText = "Button 3";
            // 
            // mB2
            // 
            this.mB2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB2.Location = new System.Drawing.Point(145, 98);
            this.mB2.Name = "mB2";
            this.mB2.Size = new System.Drawing.Size(121, 20);
            this.mB2.TabIndex = 16;
            this.mB2.Text = "_Btn2_";
            this.mB2.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mB2.WatermarkText = "Button 2";
            // 
            // mB1
            // 
            this.mB1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mB1.Location = new System.Drawing.Point(145, 45);
            this.mB1.Name = "mB1";
            this.mB1.Size = new System.Drawing.Size(121, 20);
            this.mB1.TabIndex = 14;
            this.mB1.Text = "_Btn1_";
            this.mB1.TextColor = System.Drawing.SystemColors.ControlText;
            this.mB1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.mB1.WatermarkText = "Button 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.richTextBox2);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.textboxWatermark3);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.textboxWatermark2);
            this.groupBox2.Controls.Add(this.textboxWatermark1);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(292, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(451, 232);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SettingsManager";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(122, 150);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(110, 24);
            this.button10.TabIndex = 9;
            this.button10.Text = "Remove setting";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(344, 121);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(100, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "Get all";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(238, 121);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(100, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "Add all";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(344, 19);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(100, 96);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(238, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(100, 96);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(122, 48);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(110, 23);
            this.button7.TabIndex = 5;
            this.button7.Text = "Save";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textboxWatermark3
            // 
            this.textboxWatermark3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark3.Location = new System.Drawing.Point(122, 127);
            this.textboxWatermark3.Name = "textboxWatermark3";
            this.textboxWatermark3.Size = new System.Drawing.Size(110, 20);
            this.textboxWatermark3.TabIndex = 4;
            this.textboxWatermark3.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark3.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark3.WatermarkText = "Read value";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(122, 98);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(110, 23);
            this.button6.TabIndex = 3;
            this.button6.Text = "Get Setting";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 151);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "Add Setting";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textboxWatermark2
            // 
            this.textboxWatermark2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.Location = new System.Drawing.Point(6, 127);
            this.textboxWatermark2.Name = "textboxWatermark2";
            this.textboxWatermark2.Size = new System.Drawing.Size(110, 20);
            this.textboxWatermark2.TabIndex = 2;
            this.textboxWatermark2.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark2.WatermarkText = "Value";
            // 
            // textboxWatermark1
            // 
            this.textboxWatermark1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.Location = new System.Drawing.Point(6, 100);
            this.textboxWatermark1.Name = "textboxWatermark1";
            this.textboxWatermark1.Size = new System.Drawing.Size(110, 20);
            this.textboxWatermark1.TabIndex = 2;
            this.textboxWatermark1.TextColor = System.Drawing.SystemColors.ControlText;
            this.textboxWatermark1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textboxWatermark1.WatermarkText = "Key";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Load file";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "New file";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 553);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox mL1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox mL2;
        private System.Windows.Forms.ComboBox mL3;
        private EsseivaN.Controls.TextboxWatermark mB1;
        private EsseivaN.Controls.TextboxWatermark mB2;
        private EsseivaN.Controls.TextboxWatermark mB3;
        private EsseivaN.Controls.TextboxWatermark mMsg;
        private EsseivaN.Controls.TextboxWatermark mTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button4;
        private EsseivaN.Controls.TextboxWatermark textboxWatermark2;
        private EsseivaN.Controls.TextboxWatermark textboxWatermark1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private EsseivaN.Controls.TextboxWatermark textboxWatermark3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button10;
    }
}

