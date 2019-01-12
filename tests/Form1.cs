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
            mL1.Items.Clear();
            mL2.Items.Clear();
            mL3.Items.Clear();

            mL1.Items.AddRange(Enum.GetNames(typeof(EsseivaN.Controls.Dialog.ButtonType)));
            mL2.Items.AddRange(Enum.GetNames(typeof(EsseivaN.Controls.Dialog.ButtonType)));
            mL3.Items.AddRange(Enum.GetNames(typeof(EsseivaN.Controls.Dialog.ButtonType)));

            mL1.SelectedIndex = mL2.SelectedIndex = mL3.SelectedIndex = 0;
        }

        #region UpdateChecker

        private async void button1_Click(object sender, EventArgs e)
        {

            EsseivaN.Controls.UpdateChecker updateChecker = new EsseivaN.Controls.UpdateChecker(@"http://www.esseivan.ch/files/softwares/resistortool/version.xml", "1.0");
            if (updateChecker.CheckUpdates())
            {
                if (System.Windows.Forms.MessageBox.Show("Download update ?", "Update available", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (await updateChecker.Result.DownloadUpdate())
                    {
                        Application.Exit();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to download file\nCheck your internet connection", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region DialogInput

        private void button2_Click(object sender, EventArgs e)
        {
            var t = ((EsseivaN.Controls.Dialog.ButtonType[])Enum.GetValues(typeof(EsseivaN.Controls.Dialog.ButtonType)));
            EsseivaN.Controls.Dialog.SetButton(EsseivaN.Controls.Dialog.Button.Button1, mB1.Text);
            EsseivaN.Controls.Dialog.SetButton(EsseivaN.Controls.Dialog.Button.Button2, mB2.Text);
            EsseivaN.Controls.Dialog.SetButton(EsseivaN.Controls.Dialog.Button.Button3, mB3.Text);
            label1.Text = string.Empty;
            label1.Text = EsseivaN.Controls.Dialog.ShowDialog(mMsg.Text, mTitle.Text, t[mL1.SelectedIndex], t[mL2.SelectedIndex], t[mL3.SelectedIndex]).ToString();
        }

        #endregion

        #region SettingsManager v4

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

        #region SettingsManager v3

        //Dictionary<string, setting> settingsList = new Dictionary<string, setting>();

        //class setting
        //{
        //    public int index;
        //    public int type;
        //    // type 0 : Window settings
        //    public string width;
        //    public string height;
        //    // type 1 : Boot settings
        //    public string minimize;
        //    public string onTop;

        //    public setting(int type)
        //    {
        //        this.type = type;
        //    }
        //}

        //public void generateSettings()
        //{
        //    settingsList.Add("Window settings", new setting(0) { index = 0, width = "680", height = "680" });
        //    settingsList.Add("Boot settings", new setting(1) { index = 1, minimize = "false", onTop = "true" });
        //}

        //private string getName(setting settings)
        //{
        //    switch (settings.type)
        //    {
        //        case 0:
        //            return "Window settings";
        //        case 1:
        //            return "Boot settings";
        //        default:
        //            return "Unknown";
        //    }
        //}

        //EsseivaN.Controls.SettingsManager_v3<setting> settingsManager;
        //// New file
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    // Create new file
        //    settingsManager = new EsseivaN.Controls.SettingsManager_v3<setting>(getName);

        //    generateSettings();

        //    foreach (var item in settingsList)
        //    {
        //        settingsManager.addSetting(item.Value);
        //    }
        //}

        //// Add setting to selected combobox
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    if (settingsManager == null)
        //    {
        //        return;
        //    }

        //    settingsManager.addSetting(new setting(0) { width = textboxWatermark1.Text, height = textboxWatermark2.Text });
        //}

        //// Get setting
        //private void button6_Click(object sender, EventArgs e)
        //{
        //    if (settingsManager == null)
        //    {
        //        return;
        //    }

        //    setting setting = settingsManager.getSetting(textboxWatermark1.Text);
        //    if (setting == null)
        //    {
        //        textboxWatermark3.Text = string.Empty;
        //    }
        //    else
        //    {
        //        textboxWatermark3.Text = setting.type.ToString();
        //    }
        //}

        //// Save file
        //private void button7_Click(object sender, EventArgs e)
        //{
        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        if (settingsManager == null)
        //        {
        //            return;
        //        }

        //        settingsManager.save(saveFileDialog1.FileName);
        //    }
        //}

        //// Open file
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        settingsManager = new EsseivaN.Controls.SettingsManager_v3<setting>(getName);
        //        settingsManager.load(openFileDialog1.FileName);
        //    }
        //}

        //// Add all settings
        //private void button8_Click(object sender, EventArgs e)
        //{
        //    settingsManager.addSettingRange(richTextBox1.Text);
        //}

        //// Get all settings
        //private void button9_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = settingsManager.generateFileData();
        //}

        //// Remove settings
        //private void button10_Click(object sender, EventArgs e)
        //{
        //    settingsManager.removeSetting(textboxWatermark1.Text);
        //}


        #endregion

    }
}
