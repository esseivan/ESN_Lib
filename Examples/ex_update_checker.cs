using System;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_update_checker : Form
    {
        public ex_update_checker()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (!Version.TryParse(textboxWatermark1.Text, out Version v))
            {
                MessageBox.Show("Invalid version !!");
                return;
            }

            EsseivaN_Lib.Controls.UpdateChecker updateChecker = new EsseivaN_Lib.Controls.UpdateChecker(textboxWatermark2.Text, textboxWatermark1.Text);
            if (updateChecker.CheckUpdates())
            {
                if (System.Windows.Forms.MessageBox.Show("Update available\nDownload ?", "Update available", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if (await updateChecker.Result.DownloadUpdate())
                    //{
                    //    Application.Exit();
                    //}
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Unable to download file\nCheck your internet connection", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //}
                }
            }
            else
            {
                MessageBox.Show("No update needed");
            }
        }

        private void ex_update_checker_Load(object sender, EventArgs e)
        {
            textboxWatermark1.Text = "1.0";
            textboxWatermark2.Text = @"http://www.esseivan.ch/files/softwares/resistortool/version.xml";
        }
    }
}
