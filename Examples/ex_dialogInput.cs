using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_dialogInput : Form
    {
        public ex_dialogInput()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = ((EsseivaN_Lib.Controls.Dialog.ButtonType[])Enum.GetValues(typeof(EsseivaN_Lib.Controls.Dialog.ButtonType)));
            EsseivaN_Lib.Controls.Dialog.DialogInput_SetButton(EsseivaN_Lib.Controls.Dialog.Button.Button1, mB1.Text);
            EsseivaN_Lib.Controls.Dialog.DialogInput_SetButton(EsseivaN_Lib.Controls.Dialog.Button.Button2, mB2.Text);
            EsseivaN_Lib.Controls.Dialog.DialogInput_SetButton(EsseivaN_Lib.Controls.Dialog.Button.Button3, mB3.Text);
            label1.Text = string.Empty;
            var t2 = EsseivaN_Lib.Controls.Dialog.ShowDialogInput(mMsg.Text, mTitle.Text, mDInput.Text,t[mL1.SelectedIndex], t[mL2.SelectedIndex], t[mL3.SelectedIndex]);

            label1.Text = t2.dialogResult.ToString();
            mInput.Text = t2.input;
        }

        private void ex_dialogInput_Load(object sender, EventArgs e)
        {
            mL1.Items.Clear();
            mL2.Items.Clear();
            mL3.Items.Clear();

            mL1.Items.AddRange(Enum.GetNames(typeof(EsseivaN_Lib.Controls.Dialog.ButtonType)));
            mL2.Items.AddRange(Enum.GetNames(typeof(EsseivaN_Lib.Controls.Dialog.ButtonType)));
            mL3.Items.AddRange(Enum.GetNames(typeof(EsseivaN_Lib.Controls.Dialog.ButtonType)));

            mL1.SelectedIndex = mL2.SelectedIndex = mL3.SelectedIndex = 0;
        }
    }
}
