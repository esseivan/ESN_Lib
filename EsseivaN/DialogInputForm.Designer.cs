namespace EsseivaN.Controls
{
    partial class DialogInputForm
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
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.mButton_2 = new System.Windows.Forms.Button();
            this.mButton_Cancel = new System.Windows.Forms.Button();
            this.mTextbox_Input = new System.Windows.Forms.TextBox();
            this.mButton_1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.AutoSize = true;
            this.layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layout.ColumnCount = 4;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Controls.Add(this.lblQuestion, 1, 1);
            this.layout.Controls.Add(this.mButton_2, 2, 3);
            this.layout.Controls.Add(this.mButton_Cancel, 2, 4);
            this.layout.Controls.Add(this.mTextbox_Input, 1, 5);
            this.layout.Controls.Add(this.mButton_1, 2, 2);
            this.layout.Controls.Add(this.label1, 1, 6);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 8;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Size = new System.Drawing.Size(284, 162);
            this.layout.TabIndex = 1;
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoEllipsis = true;
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuestion.Location = new System.Drawing.Point(23, 23);
            this.lblQuestion.Margin = new System.Windows.Forms.Padding(3);
            this.lblQuestion.Name = "lblQuestion";
            this.layout.SetRowSpan(this.lblQuestion, 3);
            this.lblQuestion.Size = new System.Drawing.Size(158, 54);
            this.lblQuestion.TabIndex = 0;
            // 
            // mButton_2
            // 
            this.mButton_2.AutoSize = true;
            this.mButton_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mButton_2.Location = new System.Drawing.Point(187, 53);
            this.mButton_2.Name = "mButton_2";
            this.mButton_2.Size = new System.Drawing.Size(74, 24);
            this.mButton_2.TabIndex = 11;
            this.mButton_2.Text = "btn2";
            this.mButton_2.UseVisualStyleBackColor = true;
            this.mButton_2.Visible = false;
            this.mButton_2.Click += new System.EventHandler(this.mButton_2_Click);
            // 
            // mButton_Cancel
            // 
            this.mButton_Cancel.AutoSize = true;
            this.mButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mButton_Cancel.Location = new System.Drawing.Point(187, 83);
            this.mButton_Cancel.Name = "mButton_Cancel";
            this.mButton_Cancel.Size = new System.Drawing.Size(74, 24);
            this.mButton_Cancel.TabIndex = 12;
            this.mButton_Cancel.Text = "Cancel";
            this.mButton_Cancel.UseVisualStyleBackColor = true;
            this.mButton_Cancel.Click += new System.EventHandler(this.mButton_Cancel_Click);
            // 
            // mTextbox_Input
            // 
            this.layout.SetColumnSpan(this.mTextbox_Input, 2);
            this.mTextbox_Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTextbox_Input.Location = new System.Drawing.Point(23, 113);
            this.mTextbox_Input.Name = "mTextbox_Input";
            this.mTextbox_Input.Size = new System.Drawing.Size(238, 20);
            this.mTextbox_Input.TabIndex = 1;
            // 
            // mButton_1
            // 
            this.mButton_1.AutoSize = true;
            this.mButton_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mButton_1.Location = new System.Drawing.Point(187, 23);
            this.mButton_1.Name = "mButton_1";
            this.mButton_1.Size = new System.Drawing.Size(74, 24);
            this.mButton_1.TabIndex = 10;
            this.mButton_1.Text = "btn1";
            this.mButton_1.UseVisualStyleBackColor = true;
            this.mButton_1.Click += new System.EventHandler(this.mButton_1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(23, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // DialogInputForm
            // 
            this.AcceptButton = this.mButton_1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.mButton_Cancel;
            this.ClientSize = new System.Drawing.Size(284, 162);
            this.ControlBox = false;
            this.Controls.Add(this.layout);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 170);
            this.Name = "DialogInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogInputForm_FormClosing);
            this.Load += new System.EventHandler(this.DialogInputForm_Load);
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button mButton_2;
        private System.Windows.Forms.TextBox mTextbox_Input;
        private System.Windows.Forms.Button mButton_Cancel;
        private System.Windows.Forms.Button mButton_1;
		private System.Windows.Forms.Label label1;
	}
}
