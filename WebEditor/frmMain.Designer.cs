namespace WebsiteEditor
{
    partial class frmMain
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
            this.groupVar = new System.Windows.Forms.GroupBox();
            this.txtVar = new EsseivaN.Controls.TextboxWatermark();
            this.btnVar = new System.Windows.Forms.Button();
            this.listVar = new System.Windows.Forms.CheckedListBox();
            this.txtParam = new System.Windows.Forms.Label();
            this.groupParamIndex = new System.Windows.Forms.Panel();
            this.boxRename = new System.Windows.Forms.CheckBox();
            this.boxVars = new System.Windows.Forms.CheckBox();
            this.boxContent = new System.Windows.Forms.CheckBox();
            this.btnParam = new System.Windows.Forms.Button();
            this.groupHtml = new System.Windows.Forms.GroupBox();
            this.btnHtml = new System.Windows.Forms.Button();
            this.listHtml = new System.Windows.Forms.CheckedListBox();
            this.groupContent = new System.Windows.Forms.GroupBox();
            this.btnContent = new System.Windows.Forms.Button();
            this.listContent = new System.Windows.Forms.CheckedListBox();
            this.groupParam = new System.Windows.Forms.GroupBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.listParam = new System.Windows.Forms.CheckedListBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.txtOutput = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDataDialog = new System.Windows.Forms.SaveFileDialog();
            this.openDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelContentVar = new System.Windows.Forms.Panel();
            this.groupContentVar = new System.Windows.Forms.GroupBox();
            this.btnContentVar = new System.Windows.Forms.Button();
            this.listContentVar = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupName = new System.Windows.Forms.GroupBox();
            this.btnName = new System.Windows.Forms.Button();
            this.listHtml2 = new System.Windows.Forms.CheckedListBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.updateChecker = new EsseivaN.Tools.UpdateChecker();
            this.groupVar.SuspendLayout();
            this.groupParamIndex.SuspendLayout();
            this.groupHtml.SuspendLayout();
            this.groupContent.SuspendLayout();
            this.groupParam.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelContentVar.SuspendLayout();
            this.groupContentVar.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupName.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupVar
            // 
            this.groupVar.Controls.Add(this.txtVar);
            this.groupVar.Controls.Add(this.btnVar);
            this.groupVar.Controls.Add(this.listVar);
            this.groupVar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupVar.Location = new System.Drawing.Point(3, 3);
            this.groupVar.Name = "groupVar";
            this.groupVar.Size = new System.Drawing.Size(143, 207);
            this.groupVar.TabIndex = 1;
            this.groupVar.TabStop = false;
            this.groupVar.Text = "Replace ...";
            // 
            // txtVar
            // 
            this.txtVar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVar.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtVar.Location = new System.Drawing.Point(6, 30);
            this.txtVar.Name = "txtVar";
            this.txtVar.Size = new System.Drawing.Size(50, 20);
            this.txtVar.TabIndex = 40;
            this.txtVar.TextColor = System.Drawing.SystemColors.ControlText;
            this.txtVar.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.txtVar.WatermarkText = "Enter variable";
            this.txtVar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVar_KeyDown);
            // 
            // btnVar
            // 
            this.btnVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVar.Location = new System.Drawing.Point(62, 28);
            this.btnVar.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnVar.Name = "btnVar";
            this.btnVar.Size = new System.Drawing.Size(75, 23);
            this.btnVar.TabIndex = 41;
            this.btnVar.Text = "Add variable";
            this.btnVar.UseVisualStyleBackColor = true;
            this.btnVar.Click += new System.EventHandler(this.btnVar_Click);
            // 
            // listVar
            // 
            this.listVar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listVar.CheckOnClick = true;
            this.listVar.FormattingEnabled = true;
            this.listVar.HorizontalScrollbar = true;
            this.listVar.Location = new System.Drawing.Point(6, 54);
            this.listVar.Name = "listVar";
            this.listVar.Size = new System.Drawing.Size(131, 124);
            this.listVar.TabIndex = 42;
            // 
            // txtParam
            // 
            this.txtParam.AutoSize = true;
            this.txtParam.Location = new System.Drawing.Point(101, 10);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(82, 13);
            this.txtParam.TabIndex = 3;
            this.txtParam.Text = "Replace ... in ...";
            // 
            // groupParamIndex
            // 
            this.groupParamIndex.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupParamIndex.Controls.Add(this.boxRename);
            this.groupParamIndex.Controls.Add(this.boxVars);
            this.groupParamIndex.Controls.Add(this.boxContent);
            this.groupParamIndex.Controls.Add(this.btnParam);
            this.groupParamIndex.Controls.Add(this.txtParam);
            this.groupParamIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupParamIndex.Location = new System.Drawing.Point(0, 26);
            this.groupParamIndex.Name = "groupParamIndex";
            this.groupParamIndex.Padding = new System.Windows.Forms.Padding(10);
            this.groupParamIndex.Size = new System.Drawing.Size(984, 34);
            this.groupParamIndex.TabIndex = 20;
            // 
            // boxRename
            // 
            this.boxRename.AutoSize = true;
            this.boxRename.Checked = true;
            this.boxRename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxRename.Location = new System.Drawing.Point(507, 9);
            this.boxRename.Name = "boxRename";
            this.boxRename.Size = new System.Drawing.Size(120, 17);
            this.boxRename.TabIndex = 53;
            this.boxRename.Text = "Rename output files";
            this.boxRename.UseVisualStyleBackColor = true;
            this.boxRename.CheckedChanged += new System.EventHandler(this.boxRename_CheckedChanged);
            // 
            // boxVars
            // 
            this.boxVars.AutoSize = true;
            this.boxVars.Location = new System.Drawing.Point(328, 9);
            this.boxVars.Name = "boxVars";
            this.boxVars.Size = new System.Drawing.Size(105, 17);
            this.boxVars.TabIndex = 23;
            this.boxVars.Text = "by variables in ...";
            this.boxVars.UseVisualStyleBackColor = true;
            this.boxVars.CheckedChanged += new System.EventHandler(this.boxVars_CheckedChanged);
            // 
            // boxContent
            // 
            this.boxContent.AutoSize = true;
            this.boxContent.Checked = true;
            this.boxContent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxContent.Location = new System.Drawing.Point(217, 9);
            this.boxContent.Name = "boxContent";
            this.boxContent.Size = new System.Drawing.Size(100, 17);
            this.boxContent.TabIndex = 22;
            this.boxContent.Text = "by content of ...";
            this.boxContent.UseVisualStyleBackColor = true;
            this.boxContent.CheckedChanged += new System.EventHandler(this.boxContent_CheckedChanged);
            // 
            // btnParam
            // 
            this.btnParam.Location = new System.Drawing.Point(6, 5);
            this.btnParam.Name = "btnParam";
            this.btnParam.Size = new System.Drawing.Size(89, 23);
            this.btnParam.TabIndex = 21;
            this.btnParam.Text = "Add script";
            this.btnParam.UseVisualStyleBackColor = true;
            this.btnParam.Click += new System.EventHandler(this.btnParam_Click);
            // 
            // groupHtml
            // 
            this.groupHtml.Controls.Add(this.btnHtml);
            this.groupHtml.Controls.Add(this.listHtml);
            this.groupHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupHtml.Location = new System.Drawing.Point(3, 3);
            this.groupHtml.Name = "groupHtml";
            this.groupHtml.Size = new System.Drawing.Size(143, 207);
            this.groupHtml.TabIndex = 50;
            this.groupHtml.TabStop = false;
            this.groupHtml.Text = "in ...";
            // 
            // btnHtml
            // 
            this.btnHtml.AllowDrop = true;
            this.btnHtml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHtml.Location = new System.Drawing.Point(6, 26);
            this.btnHtml.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnHtml.Name = "btnHtml";
            this.btnHtml.Size = new System.Drawing.Size(131, 23);
            this.btnHtml.TabIndex = 51;
            this.btnHtml.Text = "Add file";
            this.btnHtml.UseVisualStyleBackColor = true;
            this.btnHtml.Click += new System.EventHandler(this.btnHtml_Click);
            this.btnHtml.DragDrop += new System.Windows.Forms.DragEventHandler(this.listHtml_DragDrop);
            // 
            // listHtml
            // 
            this.listHtml.AllowDrop = true;
            this.listHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listHtml.CheckOnClick = true;
            this.listHtml.FormattingEnabled = true;
            this.listHtml.HorizontalScrollbar = true;
            this.listHtml.Location = new System.Drawing.Point(6, 54);
            this.listHtml.Name = "listHtml";
            this.listHtml.Size = new System.Drawing.Size(131, 124);
            this.listHtml.TabIndex = 52;
            this.listHtml.DragDrop += new System.Windows.Forms.DragEventHandler(this.listHtml_DragDrop);
            this.listHtml.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_DragEnter);
            // 
            // groupContent
            // 
            this.groupContent.Controls.Add(this.btnContent);
            this.groupContent.Controls.Add(this.listContent);
            this.groupContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupContent.Location = new System.Drawing.Point(0, 0);
            this.groupContent.Name = "groupContent";
            this.groupContent.Size = new System.Drawing.Size(143, 207);
            this.groupContent.TabIndex = 60;
            this.groupContent.TabStop = false;
            this.groupContent.Text = "by content of ...";
            // 
            // btnContent
            // 
            this.btnContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContent.Location = new System.Drawing.Point(6, 26);
            this.btnContent.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(131, 23);
            this.btnContent.TabIndex = 61;
            this.btnContent.Text = "Add file";
            this.btnContent.UseVisualStyleBackColor = true;
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // listContent
            // 
            this.listContent.AllowDrop = true;
            this.listContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listContent.CheckOnClick = true;
            this.listContent.FormattingEnabled = true;
            this.listContent.HorizontalScrollbar = true;
            this.listContent.Location = new System.Drawing.Point(6, 54);
            this.listContent.Name = "listContent";
            this.listContent.Size = new System.Drawing.Size(131, 124);
            this.listContent.TabIndex = 62;
            this.listContent.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listContent_ItemCheck);
            this.listContent.DragDrop += new System.Windows.Forms.DragEventHandler(this.listContent_DragDrop);
            this.listContent.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_DragEnter);
            // 
            // groupParam
            // 
            this.groupParam.Controls.Add(this.btnReplace);
            this.groupParam.Controls.Add(this.listParam);
            this.groupParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupParam.Location = new System.Drawing.Point(0, 0);
            this.groupParam.Name = "groupParam";
            this.groupParam.Size = new System.Drawing.Size(372, 213);
            this.groupParam.TabIndex = 80;
            this.groupParam.TabStop = false;
            this.groupParam.Text = "Generated scripts";
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.Location = new System.Drawing.Point(6, 31);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(357, 23);
            this.btnReplace.TabIndex = 81;
            this.btnReplace.Text = "Execute replacements";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // listParam
            // 
            this.listParam.AllowDrop = true;
            this.listParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listParam.CheckOnClick = true;
            this.listParam.FormattingEnabled = true;
            this.listParam.HorizontalScrollbar = true;
            this.listParam.Location = new System.Drawing.Point(6, 57);
            this.listParam.Name = "listParam";
            this.listParam.Size = new System.Drawing.Size(357, 124);
            this.listParam.TabIndex = 82;
            this.listParam.DragDrop += new System.Windows.Forms.DragEventHandler(this.listParam_DragDrop);
            this.listParam.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_DragEnter);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripStatusLabel,
            this.statusStripText});
            this.statusStrip.Location = new System.Drawing.Point(0, 289);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusStripStatusLabel
            // 
            this.statusStripStatusLabel.Name = "statusStripStatusLabel";
            this.statusStripStatusLabel.Size = new System.Drawing.Size(45, 17);
            this.statusStripStatusLabel.Text = "Status :";
            // 
            // statusStripText
            // 
            this.statusStripText.Name = "statusStripText";
            this.statusStripText.Size = new System.Drawing.Size(30, 17);
            this.statusStripText.Text = "IDLE";
            // 
            // panelOutput
            // 
            this.panelOutput.Controls.Add(this.txtOutput);
            this.panelOutput.Controls.Add(this.btnOutput);
            this.panelOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOutput.Location = new System.Drawing.Point(0, 60);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(984, 34);
            this.panelOutput.TabIndex = 10;
            // 
            // txtOutput
            // 
            this.txtOutput.AutoSize = true;
            this.txtOutput.Location = new System.Drawing.Point(101, 11);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(33, 13);
            this.txtOutput.TabIndex = 8;
            this.txtOutput.Text = "None";
            this.txtOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(6, 6);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(89, 23);
            this.btnOutput.TabIndex = 11;
            this.btnOutput.Text = "Select output";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // openDialog
            // 
            this.openDialog.DefaultExt = "*.html";
            this.openDialog.FileName = "Load websites files";
            this.openDialog.Filter = "HTML|*.htm;*.html|Text|*.txt|All|*.*";
            this.openDialog.Multiselect = true;
            // 
            // saveDataDialog
            // 
            this.saveDataDialog.DefaultExt = "*.webedit";
            this.saveDataDialog.Filter = "WebEdit|*.webedit|All|*.*";
            // 
            // openDataDialog
            // 
            this.openDataDialog.DefaultExt = "*.webedit";
            this.openDataDialog.FileName = "Load config file";
            this.openDataDialog.Filter = "WebEdit|*.webedit|All|*.*";
            // 
            // folderDialog
            // 
            this.folderDialog.Description = "Select the output folder for the files";
            this.folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 94);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Panel1MinSize = 425;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.groupParam);
            this.splitContainer.Panel2MinSize = 160;
            this.splitContainer.Size = new System.Drawing.Size(984, 217);
            this.splitContainer.SplitterDistance = 600;
            this.splitContainer.SplitterIncrement = 5;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 30;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupVar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelContentVar, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(596, 213);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelContentVar
            // 
            this.panelContentVar.Controls.Add(this.groupContentVar);
            this.panelContentVar.Controls.Add(this.groupContent);
            this.panelContentVar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentVar.Location = new System.Drawing.Point(450, 3);
            this.panelContentVar.Name = "panelContentVar";
            this.panelContentVar.Size = new System.Drawing.Size(143, 207);
            this.panelContentVar.TabIndex = 61;
            // 
            // groupContentVar
            // 
            this.groupContentVar.Controls.Add(this.btnContentVar);
            this.groupContentVar.Controls.Add(this.listContentVar);
            this.groupContentVar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupContentVar.Location = new System.Drawing.Point(0, 0);
            this.groupContentVar.Name = "groupContentVar";
            this.groupContentVar.Size = new System.Drawing.Size(143, 207);
            this.groupContentVar.TabIndex = 60;
            this.groupContentVar.TabStop = false;
            this.groupContentVar.Text = "by variables in ...";
            this.groupContentVar.Visible = false;
            // 
            // btnContentVar
            // 
            this.btnContentVar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContentVar.Location = new System.Drawing.Point(6, 26);
            this.btnContentVar.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnContentVar.Name = "btnContentVar";
            this.btnContentVar.Size = new System.Drawing.Size(131, 23);
            this.btnContentVar.TabIndex = 61;
            this.btnContentVar.Text = "Add file";
            this.btnContentVar.UseVisualStyleBackColor = true;
            this.btnContentVar.Click += new System.EventHandler(this.btnContentVar_Click);
            // 
            // listContentVar
            // 
            this.listContentVar.AllowDrop = true;
            this.listContentVar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listContentVar.CheckOnClick = true;
            this.listContentVar.FormattingEnabled = true;
            this.listContentVar.HorizontalScrollbar = true;
            this.listContentVar.Location = new System.Drawing.Point(6, 54);
            this.listContentVar.Name = "listContentVar";
            this.listContentVar.Size = new System.Drawing.Size(131, 124);
            this.listContentVar.TabIndex = 62;
            this.listContentVar.DragDrop += new System.Windows.Forms.DragEventHandler(this.listContentVar_DragDrop);
            this.listContentVar.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_DragEnter);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupHtml, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupName, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(149, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(298, 213);
            this.tableLayoutPanel2.TabIndex = 64;
            // 
            // groupName
            // 
            this.groupName.Controls.Add(this.btnName);
            this.groupName.Controls.Add(this.listHtml2);
            this.groupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupName.Location = new System.Drawing.Point(152, 3);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(143, 207);
            this.groupName.TabIndex = 62;
            this.groupName.TabStop = false;
            this.groupName.Text = "saved at ...";
            // 
            // btnName
            // 
            this.btnName.AllowDrop = true;
            this.btnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnName.Location = new System.Drawing.Point(6, 26);
            this.btnName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(131, 23);
            this.btnName.TabIndex = 51;
            this.btnName.Text = "Edit outputs";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // listHtml2
            // 
            this.listHtml2.AllowDrop = true;
            this.listHtml2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listHtml2.CheckOnClick = true;
            this.listHtml2.FormattingEnabled = true;
            this.listHtml2.HorizontalScrollbar = true;
            this.listHtml2.Location = new System.Drawing.Point(6, 54);
            this.listHtml2.Name = "listHtml2";
            this.listHtml2.Size = new System.Drawing.Size(131, 124);
            this.listHtml2.TabIndex = 52;
            this.listHtml2.DragDrop += new System.Windows.Forms.DragEventHandler(this.listHtml_DragDrop);
            this.listHtml2.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_DragEnter);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 26);
            this.menuStrip.TabIndex = 251;
            this.menuStrip.TabStop = true;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 19);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.AutoSize = true;
            this.panelMenu.Controls.Add(this.menuStrip);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(984, 26);
            this.panelMenu.TabIndex = 250;
            // 
            // updateChecker
            // 
            this.updateChecker.FileUrl = "";
            this.updateChecker.ProductVersion = null;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 311);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.groupParamIndex);
            this.Controls.Add(this.panelMenu);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1000, 350);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Website editor";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupVar.ResumeLayout(false);
            this.groupVar.PerformLayout();
            this.groupParamIndex.ResumeLayout(false);
            this.groupParamIndex.PerformLayout();
            this.groupHtml.ResumeLayout(false);
            this.groupContent.ResumeLayout(false);
            this.groupParam.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelContentVar.ResumeLayout(false);
            this.groupContentVar.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupName.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupVar;
        private System.Windows.Forms.Label txtParam;
        private System.Windows.Forms.Panel groupParamIndex;
        private System.Windows.Forms.GroupBox groupHtml;
        private System.Windows.Forms.Button btnHtml;
        private System.Windows.Forms.GroupBox groupContent;
        private System.Windows.Forms.Button btnContent;
        private System.Windows.Forms.CheckedListBox listHtml;
        private System.Windows.Forms.CheckedListBox listContent;
        private System.Windows.Forms.GroupBox groupParam;
        private System.Windows.Forms.CheckedListBox listVar;
        private System.Windows.Forms.Button btnVar;
        private System.Windows.Forms.Button btnParam;
        private EsseivaN.Controls.TextboxWatermark txtVar;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripText;
        private System.Windows.Forms.CheckedListBox listParam;
        private System.Windows.Forms.ToolStripStatusLabel statusStripStatusLabel;
        private System.Windows.Forms.SaveFileDialog saveDataDialog;
        private System.Windows.Forms.OpenFileDialog openDataDialog;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.Label txtOutput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.CheckBox boxVars;
        private System.Windows.Forms.CheckBox boxContent;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panelContentVar;
        private System.Windows.Forms.GroupBox groupContentVar;
        private System.Windows.Forms.Button btnContentVar;
        private System.Windows.Forms.CheckedListBox listContentVar;
        private System.Windows.Forms.GroupBox groupName;
        private System.Windows.Forms.Button btnName;
        private System.Windows.Forms.CheckedListBox listHtml2;
        private System.Windows.Forms.CheckBox boxRename;
        private EsseivaN.Tools.UpdateChecker updateChecker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

