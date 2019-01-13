using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN.Controls
{
    public partial class TextPanel : UserControl
    {
        [Description("Text displayed"), Category("Appearance"), Browsable(true)]
        public override string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        [Description("Background color"), Category("Appearance"), Browsable(true)]
        public new Color BackColor
        {
            get
            {
                return label.BackColor;
            }
            set
            {
                label.BackColor = value;
                panel1.BackColor = value;
            }
        }

        [Description("Border style"), Category("Appearance"), Browsable(true), DefaultValue(FormBorderStyle.Fixed3D)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return panel1.BorderStyle;
            }
            set
            {
                panel1.BorderStyle = value;
            }
        }

        public event EventHandler Clipboard_Delete;
        public event EventHandler Clipboard_Load;
        public event EventHandler Clipboard_Show;

        public TextPanel()
        {
            InitializeComponent();
        }

        public TextPanel(string Text,Color BackColor)
        {
            InitializeComponent();
            label.Text = Text;
            label.BackColor = BackColor;
            panel1.BackColor = BackColor;
        }

        // Get clipboard back
        private void btn_load_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(label.Text);

            //bubble the event up to the parent
            this.Clipboard_Load?.Invoke(label.Text, e);
        }

        // Delete clipboard
        private void btn_delete_Click(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            this.Clipboard_Delete?.Invoke(label.Text, e);
        }

        // Show exact content
        private void btn_show_Click(object sender, EventArgs e)
        {
            TextDialog frmDialog = new TextDialog();
            frmDialog.ShowDialog(label.Text);
            frmDialog.Dispose();

            //bubble the event up to the parent
            this.Clipboard_Show?.Invoke(label.Text, e);
        }
    }
}
