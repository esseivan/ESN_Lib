﻿using EsseivaN.Tools;
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
            var t = ((Dialog.ButtonType[])Enum.GetValues(typeof(Dialog.ButtonType)));
            Dialog.DialogConfig dialogConfig = new Dialog.DialogConfig()
            {
                CustomButton1Text = mB1.Text,
                CustomButton2Text = mB2.Text,
                CustomButton3Text = mB3.Text,
                Message = mMsg.Text,
                Title = mTitle.Text,
                Button1 = t[mL1.SelectedIndex],
                Button2 = t[mL2.SelectedIndex],
                Button3 = t[mL3.SelectedIndex],
            };

            label1.Text = string.Empty;
            label1.Text = MessageDialog.ShowDialog(dialogConfig).ToString();
        }

        private void ex_dialogInput_Load(object sender, EventArgs e)
        {
            mL1.Items.Clear();
            mL2.Items.Clear();
            mL3.Items.Clear();

            mL1.Items.AddRange(Enum.GetNames(typeof(Dialog.ButtonType)));
            mL2.Items.AddRange(Enum.GetNames(typeof(Dialog.ButtonType)));
            mL3.Items.AddRange(Enum.GetNames(typeof(Dialog.ButtonType)));

            mL1.SelectedIndex = mL2.SelectedIndex = mL3.SelectedIndex = 0;
        }
    }
}