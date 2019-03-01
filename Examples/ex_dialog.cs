using EsseivaN.Tools;
using System;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_dialog : Form
    {
        public ex_dialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = ((Message_Config.ButtonType[])Enum.GetValues(typeof(Message_Config.ButtonType)));
            var t2 = ((Message_Config.DialogIcon[])Enum.GetValues(typeof(Message_Config.DialogIcon)));
            Message_Config.DialogConfig dialogConfig = new Message_Config.DialogConfig()
            {
                CustomButton1Text = mB1.Text,
                CustomButton2Text = mB2.Text,
                CustomButton3Text = mB3.Text,
                Message = mMsg.Text,
                Title = mTitle.Text,
                Button1 = t[mL1.SelectedIndex],
                Button2 = t[mL2.SelectedIndex],
                Button3 = t[mL3.SelectedIndex],
                Icon = t2[mI1.SelectedIndex],
            };

            label1.Text = string.Empty;
            label1.Text = EsseivaN.Tools.MessageDialog.ShowDialog(dialogConfig).ToString();
        }

        private void ex_dialogInput_Load(object sender, EventArgs e)
        {
            mL1.Items.Clear();
            mL2.Items.Clear();
            mL3.Items.Clear();

            mL1.Items.AddRange(Enum.GetNames(typeof(Message_Config.ButtonType)));
            mL2.Items.AddRange(Enum.GetNames(typeof(Message_Config.ButtonType)));
            mL3.Items.AddRange(Enum.GetNames(typeof(Message_Config.ButtonType)));
            mI1.Items.AddRange(Enum.GetNames(typeof(Message_Config.DialogIcon)));

            mL1.SelectedIndex = mL2.SelectedIndex = mL3.SelectedIndex = mI1.SelectedIndex = 0;

            mL1.SelectedIndex = 1;
            mL2.SelectedIndex = 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(mMsg.Text, mTitle.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}