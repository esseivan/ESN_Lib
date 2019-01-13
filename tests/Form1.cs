using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        #region SettingsManager

        List<setting> settingsList = new List<setting>();

        class setting
        {
            public int index;
            public int type;
            // type 0 : Window settings
            public string width;
            public string height;
            // type 1 : Boot settings
            public string minimize;
            public string onTop;

            public setting(int type)
            {
                this.type = type;
            }
        }

        public void generateSettings()
        {
            settingsList.Add(new setting(0) { index = 0, width = "680", height = "680" });
            settingsList.Add(new setting(1) { index = 1, minimize = "false", onTop = "true" });
        }

        // Function to show name
        private string getName(setting settings)
        {
            // Return the name from index
            switch (settings.index)
            {
                case 0:
                    return "Window settings";
                case 1:
                    return "Boot settings";
                default:
                    return "Unknown";
            }
        }

        EsseivaN.Controls.SettingsManager<setting> settingsManager;
        // New file
        private void button3_Click(object sender, EventArgs e)
        {
            // Create new file
            settingsManager = new EsseivaN.Controls.SettingsManager<setting>(getName);

            generateSettings();

            foreach (var item in settingsList)
            {
                settingsManager.addSetting(item);
            }
        }

        // Add setting to selected combobox
        private void button5_Click(object sender, EventArgs e)
        {
            if (settingsManager == null)
            {
                return;
            }

            settingsManager.addSetting(new setting(0) { width = textboxWatermark1.Text, height = textboxWatermark2.Text });
        }

        // Get setting
        private void button6_Click(object sender, EventArgs e)
        {
            if (settingsManager == null)
            {
                return;
            }

            setting setting = settingsManager.getSetting(textboxWatermark1.Text);
            if (setting == null)
            {
                textboxWatermark3.Text = string.Empty;
            }
            else
            {
                textboxWatermark3.Text = setting.type.ToString();
            }
        }

        // Save file
        private void button7_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (settingsManager == null)
                {
                    return;
                }

                settingsManager.save(saveFileDialog1.FileName);
            }
        }

        // Open file
        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                settingsManager = new EsseivaN.Controls.SettingsManager<setting>(getName);
                settingsManager.load(openFileDialog1.FileName);
            }
        }

        // Add all settings
        private void button8_Click(object sender, EventArgs e)
        {
            settingsManager.addSettingRange(richTextBox1.Text);
        }

        // Get all settings
        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = settingsManager.generateFileData();
        }

        // Remove settings
        private void button10_Click(object sender, EventArgs e)
        {
            settingsManager.removeSetting(textboxWatermark1.Text);
        }


        #endregion
        
    }
}
