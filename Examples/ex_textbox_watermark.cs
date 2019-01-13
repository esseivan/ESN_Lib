using System;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_textbox_watermark : Form
    {
        public ex_textbox_watermark()
        {
            InitializeComponent();

            textboxWatermark3.Text = textboxWatermark1.WatermarkText;
            textBox1.BackColor = textboxWatermark1.WatermarkColor;
            textBox2.Text = textboxWatermark1.WatermarkColor.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxWatermark1.WatermarkColor = textBox1.BackColor = colorDialog1.Color;
                textBox2.Text = colorDialog1.Color.ToString();
            }
        }

        private void textboxWatermark3_TextChanged(object sender, EventArgs e)
        {
            textboxWatermark1.WatermarkText = textboxWatermark3.Text;
        }
    }
}
