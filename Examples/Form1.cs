using System;
using System.Diagnostics;
using System.Linq;
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
            string[] args = Environment.GetCommandLineArgs();

            if (args.Contains("INSTALLED"))
            {
                // Keep precedent arguments
                string args_line = string.Empty;
                if (args.Length > 1)
                {
                    args_line = string.Join(" ", args).Replace(" INSTALLED", "");
                }

                // Restart the exe without the "INSTALLED" argument
                Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, args_line);
                Close();
                return;
            }

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
                "Flags",                // 8
                "Clipboard monitor",    // 9
                "Logger",               // 10
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
                    new ex_flags().ShowDialog();
                    break;
                case 9:
                    new ex_clipboard_monitor().ShowDialog();
                    break;
                case 10:
                    new ex_logger().ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != 0)
            {
                button1.PerformClick();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listBox1.SelectedIndex != 0)
                {
                    button1.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
