﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(new string[]
            {
                "Dialog",               // 0
                "Dialog Input",         // 1
                "Settings Manager",     // 2
                "TextBox Watermark",    // 3
                "Text Dialog",          // 4
                "Text Panel",           // 5
                "Update Checker",       // 6
                "Watermark",            // 7
                "PostBuild",            // 8
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    new ex_dialog().ShowDialog();
                    break;
                case 1:
                    new ex_dialogInput().ShowDialog();
                    break;
                case 2:
                    new ex_settings_manager().ShowDialog();
                    break;
                case 3:
                    new ex_textbox_watermark().ShowDialog();
                    break;
                case 4:
                    new ex_text_dialog().ShowDialog();
                    break;
                case 5:
                    new ex_text_panel().ShowDialog();
                    break;
                case 6:
                    new ex_update_checker().ShowDialog();
                    break;
                case 7:
                    new ex_watermark().ShowDialog();
                    break;
                case 8:
                    EsseivaN_Lib.Tools.PostBuild.Main(Environment.GetCommandLineArgs());
                    break;
                default:
                    break;
            }
        }
    }
}
