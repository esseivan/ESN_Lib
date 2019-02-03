using EsseivaN.Tools;
using System;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_flags : Form
    {
        Flags flags = new Flags();
        public ex_flags()
        {
            InitializeComponent();
            numericUpDown3.Maximum = ulong.MaxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown3.Value = flags.getBits((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            textBox1.Text = flags.displayBinary();
        }

        private void ex_flags_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            flags.setBits((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            textBox1.Text = flags.displayBinary();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numericUpDown3.Value = flags.getBits((int)numericUpDown1.Value);
            textBox1.Text = flags.displayBinary();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numericUpDown3.Value = flags.getBit((int)numericUpDown1.Value) ? 1 : 0;
            textBox1.Text = flags.displayBinary();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flags.setBits((int)numericUpDown1.Value, (int)numericUpDown3.Value);
            textBox1.Text = flags.displayBinary();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            flags.setBit((int)numericUpDown1.Value, numericUpDown3.Value > 0);
            textBox1.Text = flags.displayBinary();
        }
    }
}
